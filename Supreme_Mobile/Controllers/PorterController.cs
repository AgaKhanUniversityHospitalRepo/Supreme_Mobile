using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supreme_Mobile.Models;
using Dapper;
using System.Security.Cryptography;
using System.Text;

namespace Supreme_Mobile.Controllers
{
    public class PorterController : Controller
    {
        private const string _alg = "HmacSHA256";
        private const string _salt = "2eplSzM8CXnXyNAvRJ7j";
        private const string ConfirmLoginTokenPurpose = "LC";

        System.Data.IDbConnection _db = GeneralService.DapperConnection();

        [HttpPost]
        public JsonResult PorterLogin(MyPortersModel Tokenmodel)
        {
            try
            {
                var loginResult = _db.Query<MyPorterResultModel>(";Exec p_AuthenticatePorter @UserName,@Password",
                    new
                    {
                        UserName = Tokenmodel.Username,
                        Password = GetHashedPassword(Tokenmodel.Password)
                    }).SingleOrDefault();

                Tokenmodel.Porter_ID = loginResult.Porter_ID;
                //Tokenmodel.SecurityStamp = Guid.NewGuid().ToString();

                var token = Managers.TokenManager.GeneratePorterToken(ConfirmLoginTokenPurpose, Tokenmodel);

                _db.Query(";Exec p_UpdatePorterToken @PorterID,@TokenID",
                    new
                    {
                        PorterID = Tokenmodel.Porter_ID,
                        TokenID = token,
                    });

                MyTokenResultModel tokenResult = new MyTokenResultModel();
                tokenResult.tokenAuth = token;

                Tokenmodel.Password = string.Empty;
                //logger.LogWrite(JsonConvert.SerializeObject(Tokenmodel).ToString());
                return Json(tokenResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ee)
            {
                GenericResultModel accListResult2 = new GenericResultModel();
                accListResult2.Status = "Fail";
                accListResult2.Remarks = ee.Message;
                GeneralService.WriteErrorLog(ref ee);
                return Json(accListResult2, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreatePorter(Porter porter)
        {
            try
            {
                var newPorter = _db.Query<CreatedPorterResultModel>(
                    ";Exec p_CreatePorter @Firstname,@Lastname,@Othername,@Gender,@Mobile,@Email,@UserName,@Password,@CreatedBy",
                    new
                    {
                        Firstname = porter.Firstname,
                        Lastname = porter.Lastname,
                        Othername = porter.Othername,
                        Gender = porter.Gender,
                        Mobile = porter.Mobile,
                        Email = porter.Email,
                        Username = porter.Username,
                        Password = GetHashedPassword(porter.Password),
                        CreatedBy = porter.CreatedBy
                    }).SingleOrDefault();
                porter.Password = string.Empty;
                //logger.LogWrite(JsonConvert.SerializeObject(nUsermodel).ToString());
                return Json(porter, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }
    }
}
