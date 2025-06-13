using Colabs.ProjectManagement.Domain.Entities;

namespace Colabs.ProjectManagement.Application.Mappings
{
    public static class RoleMapper
    {
        public static Role CreateAdminRole(string workspaceId)
        {
            return new Role
            {
                RoleId = Guid.NewGuid().ToString(),
                WorkspaceId = workspaceId,
                Name = "Admin",
                Description = "Workspace administrator with full privileges",
                CanManageUsers = true,
                CanManageTasks = true,
                CanManageSprints = true,
                CanManageChatrooms = true,
                CanManageDocs = true,
                CanCreateDocs = true,
                CanEditDocs = true,
                CanReadDocs = true
            };
        }

        public static Role CreateGuestRole(string workspaceId)
        {
            return new Role
            {
                RoleId = Guid.NewGuid().ToString(),
                WorkspaceId = workspaceId,
                Name = "Guest",
                Description = "Guest access throughout the workspace",
                CanManageUsers = false,
                CanManageTasks = true,
                CanManageSprints = true,
                CanManageChatrooms = true,
                CanManageDocs = true,
                CanCreateDocs = true,
                CanEditDocs = true,
                CanReadDocs = true
            };
        }
    }
}
