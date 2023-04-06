using AT.Domain;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Countries;
using MVC.Models.Friends;
using MVC.Models.States;
using MVC.Utils;
using System.Linq;

namespace MVC.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string _url;
        private readonly string _coutriesUrl;

        // GET: FriendsController
        public FriendsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = configuration.GetSection("FriendsApiUrl").Value;
            _coutriesUrl = configuration.GetSection("CountriesApiUrl").Value;
        }

        public async Task<ActionResult> Index()
        {
            var friends = await $"{_url}/friends"
                .GetJsonAsync<IEnumerable<FriendDto>>();

            return View(friends);
        }

        // GET: FriendsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var friend = await $"{_url}/friends/{id}"
                .GetJsonAsync<CreateFriendDto>();

            var states = await $"{_coutriesUrl}/states"
                .GetJsonAsync<IEnumerable<StateDto>>();

            var myFriends = await $"{_url}/friends/my-friends/{id}"
                .GetJsonAsync<IEnumerable<FriendDto>>();

            var friends = await $"{_url}/friends"
                .GetJsonAsync<IEnumerable<FriendDto>>();

            var state = await $"{_coutriesUrl}/states/{friend.StateId}"
                .GetJsonAsync<StateDto>();

            var country = await $"{_coutriesUrl}/countries/{friend.CountryId}"
                .GetJsonAsync<CountryDto>();

            friend.StateAndCountry = $"{friend.StateId}-{friend.CountryId}";

            friends = friends.Where(f => f.Id != id && !myFriends.Contains(f));

            ViewBag.State = state;
            ViewBag.Country = country;
            ViewBag.MyFriends = myFriends;
            ViewBag.Friends = friends;
            ViewBag.NumberOfFriends = myFriends.Count();
            ViewBag.TotalNumberOfFriends = friends.Count();

            ViewBag.States = states;

            return View(friend);
        }

        // GET: FriendsController/Create
        public async Task<ActionResult> Create()
        {
            var states = await $"{_coutriesUrl}/states"
                .GetJsonAsync<IEnumerable<StateDto>>();

            ViewBag.States = states;

            return View();
        }

        // POST: FriendsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateFriendDto createFriendDto)
        {
            try
            {
                var file = createFriendDto.FormFile;
                var base64 = Base64Utils.Base64(file);

                createFriendDto.PhotoBase64 = base64;

                var stateAndCountry = createFriendDto?.StateAndCountry?.Split('-');

                createFriendDto.StateId = Convert.ToInt32(stateAndCountry[0]);
                createFriendDto.CountryId = Convert.ToInt32(stateAndCountry[1]);

                var response = await $"{_url}/friends"
                    .PostJsonAsync(createFriendDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var friend = await $"{_url}/friends/{id}"
                .GetJsonAsync<CreateFriendDto>();

            var states = await $"{_coutriesUrl}/states"
                .GetJsonAsync<IEnumerable<StateDto>>();

            var myFriends = await $"{_url}/friends/my-friends/{id}"
                .GetJsonAsync<IEnumerable<FriendDto>>();

            var friends = await $"{_url}/friends"
                .GetJsonAsync<IEnumerable<FriendDto>>();

            var state = await $"{_coutriesUrl}/states/{friend.StateId}"
                .GetJsonAsync<StateDto>();

            var country = await $"{_coutriesUrl}/countries/{friend.CountryId}"
                .GetJsonAsync<CountryDto>();

            friend.StateAndCountry = $"{friend.StateId}-{friend.CountryId}";

            friends = friends.Where(f => f.Id != id && !myFriends.Contains(f));

            ViewBag.State = state;
            ViewBag.Country = country;
            ViewBag.MyFriends = myFriends;
            ViewBag.Friends = friends;
            ViewBag.NumberOfFriends = myFriends.Count();
            ViewBag.TotalNumberOfFriends = friends.Count();

            ViewBag.States = states;

            return View(friend);
        }

        // POST: FriendsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateFriendDto updateFriendDto)
        {
            try
            {
                if (updateFriendDto.FormFile != null)
                {
                    var file = updateFriendDto.FormFile;
                    updateFriendDto.PhotoBase64 = Base64Utils.Base64(file);
                }

                var stateAndCountry = updateFriendDto?.StateAndCountry?.Split('-');

                updateFriendDto.StateId = Convert.ToInt32(stateAndCountry[0]);
                updateFriendDto.CountryId = Convert.ToInt32(stateAndCountry[1]);

                var response = await $"{_url}/friends/{id}"
                    .PutJsonAsync(updateFriendDto);

                return RedirectToAction(nameof(Index));
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(updateFriendDto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(updateFriendDto);
            }
        }

        // GET: FriendsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var friend = await $"{_url}/friends/{id}"
                .GetJsonAsync<FriendDto>();

            return View(friend);
        }

        // POST: FriendsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FriendDto friendDto)
        {
            try
            {
                var friend = await $"{_url}/friends/{friendDto.Id}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddToFriends(int friendId, int newFriendId)
        {
            var friend = await $"{_url}/friends/{friendId}"
                .GetJsonAsync<FriendDto>();

            var newFriend = await $"{_url}/friends/{newFriendId}"
                .GetJsonAsync<FriendDto>();

            return View(new AddToMyFriendsDto
            {
                Friend = friend,
                NewFriend = newFriend
            });
        }

        // POST: FriendsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToFriends(AddToMyFriendsDto addToMyFriends)
        {
            try
            {
                var friendId = addToMyFriends?.FriendId;
                var newFriendId = addToMyFriends?.NewFriendId;

                var response = await $"{_url}/friends/my-friends/{friendId}/{newFriendId}"
                    .PutJsonAsync(null);

                return RedirectToAction(nameof(Details), new { id = friendId });
            }
            catch
            {
                return View(addToMyFriends);
            }
        }

        public async Task<ActionResult> RemoveFromFriends(int friendId, int oldFriendId)
        {
            var friend = await $"{_url}/friends/{friendId}"
                .GetJsonAsync<FriendDto>();

            var oldFriend = await $"{_url}/friends/{oldFriendId}"
                .GetJsonAsync<FriendDto>();

            return View(new RemoveFromFriendsDto
            {
                Friend = friend,
                OldFriend = oldFriend
            });
        }

        // POST: FriendsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveFromFriends(RemoveFromFriendsDto removeFromFriends)
        {
            try
            {
                var friendId = removeFromFriends?.FriendId;
                var oldFriendId = removeFromFriends?.OldFriendId;

                var response = await $"{_url}/friends/my-friends/{friendId}/{oldFriendId}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Details), new { id = friendId });
            }
            catch
            {
                return View(removeFromFriends);
            }
        }
    }
}
