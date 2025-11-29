using AutoMapper;
using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Dtos.Poop;
using ShitWithFriendAPI.Dtos.Group;
using ShitWithFriendAPI.Dtos.Group;
using ShitWithFriendAPI.Dtos.UserGroupEmoji;
using ShitWithFriendAPI.Dtos.Game;
using ShitWithFriendAPI.Dtos.HighScore;
using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();

            CreateMap<Poop, PoopDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<CreatePoopRequestDto, Poop>();

            CreateMap<User, UserUsernameDto>();
            CreateMap<UserGroupEmoji, UserGroupEmojiDto>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group.Name));
            CreateMap<Group, GroupDto>();
            CreateMap<CreateGroupRequestDto, Group>();
            CreateMap<UpdateFroupRequest, Group>();

            CreateMap<Game, GameDto>();
            CreateMap<CreateGameRequestDto, Game>();
            CreateMap<UpdateGameRequestDto, Game>();

            CreateMap<HighScore, HighScoreDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game.Name));
            CreateMap<CreateHighScoreRequestDto, HighScore>();
            CreateMap<UpdateHighScoreRequestDto, HighScore>();
        }
    }
}
