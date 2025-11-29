using ShitWithFriendAPI.Dtos.Group;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IGroupService
    {
        IQueryable<GroupDto> GetGroups(int pageNumber, int pageSize);
        GroupDto GetGroupById(Guid id);
        GroupDto GetGroupByName(string name);
        GroupDto CreateGroup(CreateGroupRequestDto createGroupRequestDto);
        GroupDto UpdateGroup(Guid id, UpdateFroupRequest updateGroupRequestDto);
        void DeleteGroup(Guid id);
    }
}
