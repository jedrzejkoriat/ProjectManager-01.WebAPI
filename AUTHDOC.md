# ProjectManager-01.WebAPI - Auth Documentation

The purpose of this document is to describe how the Authorization and Authentication works in ProjectManager application.

## Table of Contents

- [General Information](#general-information)
- [Global Authorization](#global-authorization)
- [Project-Scoped Authorization](#project-scoped-authorization)
- [Example (Create Comment Authorization)](#example-create-comment-authorization)
- [Database Schema](#database-schema)

## General Information

**ProjectManager** is an application that is similar to well-known softwares like Jira, Asana etc. The purpose of it is to manage the Projects by the companies or other organizations. This kind of applications need to have a strict Authorization system that prevents Users from performing specific operations, or from accessing resources that shouldn't be accessed by them.

**ProjectManager** has two types of Authorization - **Global Authorization** and **Project-Scoped Authorization**. These Authorizations are used together in the requests. Roles and Permissions, that are used for these Authorizations, are encoded as JWT Token and passed to the Authorization Header after successful Authentication.

---

## Global Authorization
The heart of this Authorization is the **Roles** table. This is pretty simple and straight-forward - only one **Role** is allowed per **User**.

### Database Relationship Structure ([Database Schema](#database-schema))
- **Users** have one-to-one relationship with **UserRoles**
- **UserRoles** have many-to-one relationship with **Roles**

![image](https://github.com/user-attachments/assets/b5d2a4cf-b878-43df-b1ab-5a0d9e3a9af2)

### Explanation
The Authorization comes out of the box in ASP.NET Core - after putting `[Authorize(Roles=Roles.X)]` attribute above the Controller or specific Endpoint, the application will require from User to have a valid JWT Bearer Token that has the **X** Role. Application won't pass the request further if this requirement is not met and will send Unauthorized response.

![image](https://github.com/user-attachments/assets/7acc8b6a-738d-4397-beb9-ad0d5ba19418)


### Available Roles:
- Admin
- User

---

## Project-Scoped Authorization
This Authorization system is more complex and is based on the **Permissions** that are assigned to specific **Project**. The center of this system is **ProjectRoles**. One **User** can have multiple **ProjectRoles**, and one **ProjectRole** can have multiple **Permissions**. **ProjectRole** can have only one **Project** assigned.

### Database Relationship Structure ([Database Schema](#database-schema))
- **Users** have one-to-many relationship with **ProjectUserRoles**
- **ProjectUserRoles** have one-to-many relationship with **ProjectRoles**
- **ProjectRoles** have one-to-many relationship with **ProjectRolePermissions** and one-to-one relationship with **Projects**
- **ProjectRolePermissions** have many-to-one relationship with **Permissions**

![image](https://github.com/user-attachments/assets/3568ed79-c530-4574-8a24-3986150f5e46)

### Explanation
Only Endpoints that have `[Authorize(Policy=Permissions)]` attribute above them have to handle Project-Scoped Authorization. These Endpoints also require to have **ProjectId** in their route. 

![image](https://github.com/user-attachments/assets/e191ca43-5026-4020-ab06-9c9ff7153ddb)

1. First step of this Authorization is handled by `Application\ProjectPermissionHandler`

   It retrieves the **ProjectId** from the route, and all claims with **ProjectPermission** key from JWT Token. **PermissionRequirement** is passed as an argument.

   ![image](https://github.com/user-attachments/assets/c6aa14c3-343f-475d-8812-0d79b942f361)

   Then it matches the **ProjectId** and **Permission** with the claim values, that are saved by JWT Token in this format `ProjectId:Permission`. If it finds a match, then the User is Authorized and the request proceeds.

   ![image](https://github.com/user-attachments/assets/14a71374-8353-41bf-9d65-3d8befe7afbd)

2. Second step of Project-Scoped Authorization is handled by `Application\ProjectAccessValidator`

    After successful Authorization, the **ProjectId** from route is passed through Controller to adequate Service. Service passes the **ProjectId** and **ResourceId** to the Validator, where it retrieves the **ProjectId** of the Project that this Resource belongs to. Then it matches **ProjectId** from route and **ProjectId** of the resource. If it doesn't match it throws the Exception and the request is stopped.

   ![image](https://github.com/user-attachments/assets/e2409b12-18e6-491f-9b07-828461a4afa7)

    The main goal of this step is to prevent the Parameter Tampering attacks, where attackers could try changing the **ProjectId** in route in order to use Permissions from other Projects to modify Resources or to get access to Resources that they should not have access to.

### Available Permissions
- ReadComment
- WriteComment
- ReadTag
- WriteTag
- ReadProject
- ReadTicketRelation
- WriteTicketRelation
- ReadTicket
- WriteTicket
- DeleteTicket
- ReadTicketTag
- WriteTicketTag

---

## Example (Create Comment Authorization)
1. **Authenticate as a User with Permissions to some Project**

    In seed testdata there is a **User** that is called `superuser`. It has a `User` **Role**, and one **ProjectUserRole** that is related to one **Project** and all possible **Permissions**. If the login is successful, JWT Token encodes UserClaims basing on this Dto model:

    `public sealed record UserClaimsDto(Guid UserId, string Role, List<string> ProjectPermissions)`

    - **UserId** value is added to claims with **sub** as key
    - **Role** value is added with **Role** as key
    - **ProjectPermissions** come already in specific format: `ProjectId:Permission` and they are added as values with **ProjectPermission** key

    This is how the List of **Claims** looks like before encoding:

    ![image](https://github.com/user-attachments/assets/16b08b03-a5ef-4e3b-a129-d79a8c9c268f)

    So here for example `{ProjectPermission: 94e4c030-7ae2-46fd-b458-f80f9fbdefde:WriteComment}` we have **Permission** to Create, Edit or Delete **Comments** within the `94e4c030-7ae2-46fd-b458-f80f9fbdefde` Project scope (let's call it **X Project**).

2. **Send Create Comment request**

    To CreateComment, we have to choose the **Ticket** that is in scope of the **X Project**. For example we have **Ticket** `55555555-5555-5555-5555-555555555555` (let's call it **Y Ticket**). In body we pass **Y TicketId**. In route we pass the **X ProjectId**. In Authorization header goes JWT Token.

3. **Authorization**

    In this part the request goes through two layers of Authorization:

     A) **Global Authorization**: `[Authorize]` attribute above the Controller validates the JWT Token - this Controller just needs an Authorized user, no need to specify the Role.
   
     B) **Project-Scoped Authorization**: `[Authorize(Policy=Permissions.WriteComment)]` attribute above the Endpoint calls the `HandleRequirementAsync` method in `ProjectPermissionHandler`. It gets the **X ProjectId** from the Route and the **Claims** with **ProjectPermission** as a key from the token. **PermissionName** is passed as an argument of the method. Now the **X ProjectId** and **PermissionName** is compared with retrieved **ProjectPermissions**.

     - **X ProjectId**: 94e4c030-7ae2-46fd-b458-f80f9fbdefde
     - **PermissionName**: WriteComment
     - **Claim that we search**: "94e4c030-7ae2-46fd-b458-f80f9fbdefde:WriteComment"

     If it is found then this part of validation is successful.

4. **X ProjectId Validation**

    **X ProjectId** from route is passed through the Controller to Service along with the **CommentCreateDto** from body. This **Comment** model has an **Y TicketId**. So the **X ProjectId** and **Y TicketId** are passed to the `ProjectAccessValidator` where the **Y TicketId** is used to get the **Ticket.ProjectId** and then it is compared with the **X ProjectId**. If it is successful then the **X ProjectId** is validated and whole request is Authorized.

---

## Database Schema

![authschema](https://github.com/user-attachments/assets/a776a787-fb2e-49e9-8f65-293564509359)

---


