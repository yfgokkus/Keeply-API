using AutoMapper;
using DataTransferObjects;
using DiaryAppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;

namespace DiaryApp.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryAppController : ControllerBase
    {
        private readonly IMapper _mapper;
        public DiaryAppController(IMapper mapper)
        {
            _mapper = mapper;
        }

        
        [HttpGet]
        [Route("getusers")]
        public List<UserDTO> GetUserList()
        {
            using(var DbContext = new KeeplyDbContext())
            {
                var users = DbContext.Users.ToList();
                var myList = new List<UserDTO>();
                users.ForEach(e => myList.Add(_mapper.Map<UserDTO>(e)));
                return myList;
            }
        }

        [HttpGet]
        [Route("getnotelist")]
        public List<DailyNoteDTO> GetNoteList(int id)
        {
            using (var DbContext = new KeeplyDbContext())
            {
                var notes = DbContext.DailyNotes.Where(w => w.UserId == id).ToList();
                var myList = new List<DailyNoteDTO>();
                notes.ForEach(e => myList.Add(_mapper.Map<DailyNoteDTO>(e)));
                return myList;
            }
        }


        [HttpGet]
        [Route("getuserbyid")]
        public UserDTO GetUserByID(int id)
        {
            using (var DbContext = new KeeplyDbContext())
            {
                var user = DbContext.Users.Where(u => u.Id == id).FirstOrDefault();
                var myUser = _mapper.Map<UserDTO>(user);
                return myUser;
            }
        }

        [HttpGet]
        [Route("getnotebyid")]
        public DailyNoteDTO GetNoteById(int id)
        {
            using (var DbContext = new KeeplyDbContext())
            {
                var note = DbContext.DailyNotes.Where(u => u.Id == id).FirstOrDefault();
                var myNote = _mapper.Map<DailyNoteDTO>(note);
                return myNote;
            }
        }


        [HttpPut]
        [Route("register")]
        public User Register([FromBody] UserDTO userDTO)
        {
            using (var DbContext = new KeeplyDbContext())
            {
                var myUser = _mapper.Map<User>(userDTO);
                DbContext.Users.Add(myUser);
                DbContext.SaveChanges();
                return myUser; 
            }
        }

        [HttpPost]
        [Route("login")]
        public LoginResponseObject Login(string UserName, string Password)
        {
            using ( var DbContext = new KeeplyDbContext())
            {
                var user = DbContext.Users.Where(u => u.Username.Equals(UserName) && u.Password.Equals(Password)).FirstOrDefault();

                if (user != null) return new LoginResponseObject(user.Id);
                else return new LoginResponseObject(false);
            }
        }

        [HttpPost]
        [Route("savenote")]
        public DailyNote SaveNote([FromBody] DailyNoteDTO noteDTO)
        {
            using(var DbContext = new KeeplyDbContext())
            {   
                var myNote = _mapper.Map<DailyNote>(noteDTO);
                DbContext.DailyNotes.Add(myNote);
                DbContext.SaveChanges();
                return myNote;
            }
        }

        [HttpDelete]
        [Route("deletenote")]

        public Boolean DeleteNote(int id)
        {
            using (var DbContext = new KeeplyDbContext())
            {
                var myNote = DbContext.DailyNotes.Where(u => u.Id == id).FirstOrDefault();
                if (myNote == null) return false;
                DbContext.DailyNotes.Remove(myNote);
                DbContext.SaveChanges();
                return true;
            }
        }

    }
}
 