using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using koala.application.common;
using mq.application.common;
using mq.application.service;
using mq.application.service.Interface;
using mq.model.dbentity;
using mq.model.viewentity;
using mq.model.viewentity.employeebg;

namespace mq.ui.employeebg.Controllers
{
    public class UserController : Controller
    {
        private readonly IBgRoleService _bgRoleService;
        private readonly IBgAreaService _areaService;
        private readonly IBgShopService _bgShopService;
        private readonly IBgDepartmentService _bgDepartmentService;
        private readonly IBgUserService _bgUserService;
        private readonly IBgVUserAreaRoleDepartmentService _bgVUser;
        public UserController(IBgRoleService bgRoleService, IBgAreaService areaService, IBgShopService bgShopService, IBgDepartmentService bgDepartmentService, IBgUserService bgUserService, IBgVUserAreaRoleDepartmentService bgVUser)
        {
            _bgRoleService = bgRoleService;
            _areaService = areaService;
            _bgShopService = bgShopService;
            _bgDepartmentService = bgDepartmentService;
            _bgUserService = bgUserService;
            _bgVUser = bgVUser;
        }

        // GET: User
        public ActionResult Add()
        {
            UserAddEntity entity = new UserAddEntity();
            entity.RoleList = _bgRoleService.List();
            entity.DepartmentList = _bgDepartmentService.GetListDepartment();
            entity.AreaList = _areaService.List();
            if (entity.AreaList != null && entity.AreaList.Count > 0)
            {
                entity.ShopList = _bgShopService.List(entity.AreaList[0].ID);
            }
            return View(entity);
        }

        public ActionResult List()
        {
            
            return View();
        }

        public ActionResult GetPartialList()
        {
            List<V_User_Area_Role_Department_Shop> list = _bgVUser.GetList();
            return PartialView(list);
        }


        public JsonResult AddUser()
        {
            JsonUserAddUserEntity json = new JsonUserAddUserEntity();
            string name = CommonHelper.GetPostValue("username");
            string password = CommonHelper.GetPostValue("password");
            string realname = CommonHelper.GetPostValue("realname");
            string phone = CommonHelper.GetPostValue("phone");
            string email = CommonHelper.GetPostValue("email");
            int roleid = CommonHelper.GetPostValue("roleid").ToInt(-1);
            int areaid = CommonHelper.GetPostValue("areaid").ToInt(-1);
            int shopid = CommonHelper.GetPostValue("shopid").ToInt(-1);
            int departmentId = CommonHelper.GetPostValue("departmentId").ToInt(-1);

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || roleid < 0 || areaid < 0 || shopid < 0 || departmentId < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

            name = HttpUtility.UrlDecode(name);
            realname = HttpUtility.UrlDecode(realname);

            T_BG_User user = new T_BG_User();
            user.Phone = phone;
            user.Name = name;
            user.RealName = realname;
            user.PassWord = password;
            user.Email = email;
            user.RoleID = roleid;
            user.ShopID = shopid;
            user.Status = 0;
            user.AddTime = DateTime.Now;
            user.IsDel = 0;
            user.DepartmentId = departmentId;
            user.AreaId = areaid;

            bool result = _bgUserService.Add(user);

            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "添加成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "添加失败！";

            }
            return Json(json);
        }
        public ActionResult GetShopList()
        {
            long areaId = CommonHelper.GetPostValue("areaId").ToLong(-1L);
            List<T_BG_Shop> shopList = _bgShopService.List(areaId);
            return PartialView(shopList);
        }

        public JsonResult CheckUserName()
        {
            string username = CommonHelper.GetPostValue("username");
            username = HttpUtility.UrlDecode(username);
            if (string.IsNullOrEmpty(username))
            {
                return Json(new { ErrorCode = "E000", ErrorMsg = "用户名不能为空" });
            }
            bool result = _bgUserService.Check(username);
            if (result)
            {
                return Json(new { ErrorCode = "E001", ErrorMsg = "用户名已存在！" });
            }
            else
            {
                return Json(new { ErrorCode = "E000", ErrorMsg = "用户名不能为空" });
            }
        }

        public JsonResult Pass()
        {
            JsonUserPassEntity json=new JsonUserPassEntity();
            long id = CommonHelper.GetPostValue("id").ToLong(-1L);
            int state = CommonHelper.GetPostValue("state").ToInt(0);
            if (id < 0)
            {
                json.ErrorCode = "E001";
                json.ErrorMessage = "参数不全！";
                return Json(json);
            }

            T_BG_User user = _bgUserService.GetUserById(id);
            if (user==null)
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "未获得该对象，请刷新页面！";
                return Json(json);
            }
            user.Status = state;
            user.ApproveName = LoginHelper.UserName;
            user.ApproveTime=DateTime.Now;
            bool result = _bgUserService.Update(user);
            if (result)
            {
                json.ErrorCode = "E000";
                json.ErrorMessage = "成功！";
            }
            else
            {
                json.ErrorCode = "E002";
                json.ErrorMessage = "失败，请刷新页面！";
            } 
            return Json(json);
        }
    }
}