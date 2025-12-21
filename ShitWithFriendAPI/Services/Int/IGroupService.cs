using ShitWithFriendAPI.Dtos.Group;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IGroupService
    {
        IQueryable<GroupDto> GetGroups(int pageNumber, int pageSize, Guid? userId = null);
        GroupDto GetGroupById(Guid id);
        GroupDto GetGroupByName(string name);
        GroupDto CreateGroup(CreateGroupRequestDto createGroupRequestDto, Guid? creatorUserId = null);
        GroupDto UpdateGroup(Guid id, UpdateFroupRequest updateGroupRequestDto);
        void DeleteGroup(Guid id);
    }
}
