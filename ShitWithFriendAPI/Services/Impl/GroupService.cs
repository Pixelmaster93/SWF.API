using AutoMapper;
using ShitWithFriendAPI.Dtos.Group;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public GroupDto CreateGroup(CreateGroupRequestDto createGroupRequestDto, Guid? creatorUserId = null)
        {
            var group = _mapper.Map<Group>(createGroupRequestDto);
            
            if (creatorUserId.HasValue)
            {
                var creator = _userRepository.GetById(creatorUserId.Value).FirstOrDefault();
                if (creator != null)
                {
                    group.Users = new List<User> { creator };
                    group.Administrators = new List<User> { creator };
                }
            }

            var createdGroup = _groupRepository.Add(group);
            _groupRepository.SaveChanges();
            return _mapper.Map<GroupDto>(createdGroup);
        }

        public void DeleteGroup(Guid id)
        {
            var group = _groupRepository.GetById(id).FirstOrDefault();
            if (group != null)
            {
                _groupRepository.Delete(group);
                _groupRepository.SaveChanges();
            }
        }

        public GroupDto GetGroupById(Guid id)
        {
            var group = _groupRepository.GetById(id).FirstOrDefault();
            return _mapper.Map<GroupDto>(group);
        }

        public GroupDto GetGroupByName(string name)
        {
            var group = _groupRepository.GetGroupByName(name).FirstOrDefault();
            return _mapper.Map<GroupDto>(group);
        }

        public IQueryable<GroupDto> GetGroups(int pageNumber, int pageSize, Guid? userId = null)
        {
            var query = _groupRepository.GetAll();

            if (userId.HasValue)
            {
                query = query.Where(g => g.Users.Any(u => u.Id == userId.Value));
            }

            var groups = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return _mapper.ProjectTo<GroupDto>(groups);
        }

        public GroupDto UpdateGroup(Guid id, UpdateFroupRequest updateGroupRequestDto)
        {
            var group = _groupRepository.GetById(id).FirstOrDefault();
            if (group == null) return null;

            _mapper.Map(updateGroupRequestDto, group);
            _groupRepository.SaveChanges();
            return _mapper.Map<GroupDto>(group);
        }
    }
}
