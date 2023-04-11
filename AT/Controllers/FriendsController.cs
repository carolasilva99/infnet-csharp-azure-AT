using AT.Domain;
using AutoMapper;
using CountriesApi.Services;
using FriendsApi.DTOs;
using FriendsAPI.DTOs;
using FriendsAPI.Models;
using FriendsAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FriendsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendsService _friendsService;

        public FriendsController(IMapper mapper, IFriendsService friendsService)
        {
            _mapper = mapper;
            _friendsService = friendsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FriendDto>> List()
        {
            return Ok(_mapper.Map<IEnumerable<FriendDto>>(_friendsService.List()));
        }

        [HttpGet("count")]
        public ActionResult<FriendsCountDto> Count()
        {
            var numberOfFriends = _friendsService.Count();
            return Ok(new FriendsCountDto{ NumberOfFriends = numberOfFriends });
        }

        [HttpGet("{id}")]
        public ActionResult<FriendDto> Get(int id)
        {
            return Ok(_mapper.Map<FriendDto>(_friendsService.GetById(id)));
        }

        [HttpGet("my-friends/{id}")]
        public ActionResult<IEnumerable<FriendDto>> List(int id)
        {
            return Ok(_mapper.Map<IEnumerable<FriendDto>>(_friendsService.GetMyFriends(id)));
        }

        [HttpPost]
        public async Task<ActionResult<FriendDto>> Post([FromBody] CreateFriendDto state)
        {
            var photoId = await BlobsService.Upload(state.PhotoBase64, PhotoTypeEnum.STATE_FLAG);
            var mappedFriend = _mapper.Map<Friend>(state);

            mappedFriend.PhotoId = photoId;

            var createdFriend = _mapper.Map<FriendDto>(_friendsService.Create(mappedFriend));
            return CreatedAtAction(nameof(Get), new { id = createdFriend.Id }, createdFriend);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FriendDto>> Put(int id, [FromBody] CreateFriendDto state)
        {
            var photoId = await BlobsService.Upload(state.PhotoBase64, PhotoTypeEnum.STATE_FLAG);
            var mappedFriend = _mapper.Map<Friend>(state);

            mappedFriend.Id = id;
            mappedFriend.PhotoId = photoId;

            return Ok(_mapper.Map<FriendDto>(_friendsService.Update(mappedFriend)));
        }

        [HttpPut("my-friends/{id}/{newFriendId}")]
        public ActionResult AddFriendToFriendsList(int id, int newFriendId)
        {
            _friendsService.AddToMyFriendsList(id, newFriendId);
            return Ok();
        }

        [HttpDelete("my-friends/{id}/{oldFriendId}")]
        public ActionResult RemoveFromMyFriendsList(int id, int oldFriendId)
        {
            _friendsService.RemoveFromMyFriendsList(id, oldFriendId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _friendsService.Delete(id);
            return Ok();
        }
    }
}
