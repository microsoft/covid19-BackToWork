using System;
using System.Data;
using System.Threading.Tasks;
using BackToWorkFunctions.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel.Resolution;
using System.IO;

namespace BackToWorkFunctions.Helper
{
    public static class DbHelper
    {
        public static bool PostDataAsync<T>(T model, string ops)
        {
            string errorMsg;
            try
            {
                string sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);                
                if (!String.IsNullOrEmpty(sqlConnectionString))
                {
                    switch (ops)
                    {
                        case Constants.postUserInfo:                            
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("PostUserInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = typeof(T).GetProperty("UserId").GetValue(model);
                                    cmd.Parameters.Add("@FullName", SqlDbType.VarChar, 100).Value = typeof(T).GetProperty("FullName").GetValue(model);
                                    cmd.Parameters.Add("@YearOfBirth", SqlDbType.Int, 4).Value = typeof(T).GetProperty("YearOfBirth").GetValue(model);
                                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100).Value = typeof(T).GetProperty("EmailAddress").GetValue(model);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                }
                            }                       
                            break;
                        case Constants.postLabTestInfo:
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("PostLabTestInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = typeof(T).GetProperty("UserId").GetValue(model);
                                    cmd.Parameters.Add("@DateOfEntry", SqlDbType.DateTime).Value = typeof(T).GetProperty("DateOfEntry").GetValue(model);
                                    cmd.Parameters.Add("@TestType", SqlDbType.VarChar, 30).Value = typeof(T).GetProperty("TestType").GetValue(model);
                                    cmd.Parameters.Add("@TestDate", SqlDbType.DateTime).Value = typeof(T).GetProperty("TestDate").GetValue(model);
                                    cmd.Parameters.Add("@TestResult", SqlDbType.VarChar, 10).Value = typeof(T).GetProperty("TestResult").GetValue(model);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            break;
                        case Constants.postRequestStatus:                           
                             using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("PostRequestStatus", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = typeof(T).GetProperty("UserId").GetValue(model);
                                    cmd.Parameters.Add("@DateOfEntry", SqlDbType.DateTime).Value = typeof(T).GetProperty("DateOfEntry").GetValue(model);
                                    cmd.Parameters.Add("@ReturnRequestStatus", SqlDbType.VarChar, 10).Value = typeof(T).GetProperty("UserId").GetValue(model);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            break;
                        case Constants.postSymptomsInfo:                            
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("PostSymptomsInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = typeof(T).GetProperty("UserId").GetValue(model);
                                    cmd.Parameters.Add("@DateOfEntry", SqlDbType.DateTime).Value = typeof(T).GetProperty("DateOfEntry").GetValue(model);
                                    cmd.Parameters.Add("@UserIsExposed", SqlDbType.Bit).Value = typeof(T).GetProperty("UserIsExposed").GetValue(model);
                                    cmd.Parameters.Add("@ExposureDate", SqlDbType.DateTime).Value = typeof(T).GetProperty("ExposureDate").GetValue(model);
                                    cmd.Parameters.Add("@QuarantineStartDate", SqlDbType.DateTime).Value = typeof(T).GetProperty("QuarantineStartDate").GetValue(model);
                                    cmd.Parameters.Add("@QuarantineEndDate", SqlDbType.DateTime).Value = typeof(T).GetProperty("QuarantineEndDate").GetValue(model);
                                    cmd.Parameters.Add("@IsSymptomatic", SqlDbType.Bit).Value = typeof(T).GetProperty("IsSymptomatic").GetValue(model);
                                    cmd.Parameters.Add("@SymptomFever", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomFever").GetValue(model);
                                    cmd.Parameters.Add("@SymptomCough", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomCough").GetValue(model);
                                    cmd.Parameters.Add("@SymptomShortnessOfBreath", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomShortnessOfBreath").GetValue(model);
                                    cmd.Parameters.Add("@SymptomChills", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomChills").GetValue(model);
                                    cmd.Parameters.Add("@SymptomMusclePain", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomMusclePain").GetValue(model);
                                    cmd.Parameters.Add("@SymptomSoreThroat", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomSoreThroat").GetValue(model);
                                    cmd.Parameters.Add("@SymptomLossOfSmellTaste", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomLossOfSmellTaste").GetValue(model);
                                    cmd.Parameters.Add("@SymptomVomiting", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomVomiting").GetValue(model);
                                    cmd.Parameters.Add("@SymptomDiarrhea", SqlDbType.Bit).Value = typeof(T).GetProperty("SymptomDiarrhea").GetValue(model);
                                    cmd.Parameters.Add("@Temperature", SqlDbType.Decimal).Value = typeof(T).GetProperty("Temperature").GetValue(model);
                                    cmd.Parameters.Add("@UserIsSymptomatic", SqlDbType.Bit).Value = typeof(T).GetProperty("UserIsSymptomatic").GetValue(model);
                                    cmd.Parameters.Add("@ClearToWorkToday", SqlDbType.Bit).Value = typeof(T).GetProperty("ClearToWorkToday").GetValue(model);
                                    cmd.Parameters.Add("@GUID", SqlDbType.VarChar).Value = typeof(T).GetProperty("GUID").GetValue(model);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            break;
                        default:
                            errorMsg = "Error in writing data to database";
                            throw new Exception(errorMsg);
                    }
                    return true;
                }                
                errorMsg = "Error: SQL Connection Parameters not found in Configuration";
                throw new ArgumentNullException(errorMsg);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<T> GetDataAsync<T>(string ops, string paramString)
        {
            try
            {
                string errorMsg;
                string sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);
                if (!String.IsNullOrEmpty(sqlConnectionString))
                {
                    switch (ops)
                    {
                        case Constants.getUserInfo:                            
                            UserInfo userInfo = new UserInfo();
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("GetUserInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = paramString;
                                    cmd.Prepare();
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader != null)
                                        {
                                            while (reader.Read())
                                            {
                                                userInfo.UserId = reader["UserId"].ToString();
                                                userInfo.FullName = reader["FullName"].ToString();
                                                userInfo.YearOfBirth = (int)reader["YearOfBirth"];
                                                userInfo.EmailAddress = reader["EmailAddress"].ToString();
                                            }
                                        }
                                        return (T)Convert.ChangeType(userInfo, typeof(T));
                                    }
                                }
                            }
                        case Constants.getLabTestInfo:                            
                            LabTestInfo labTestInfo = new LabTestInfo();
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("GetLabTestInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = paramString;
                                    cmd.Prepare();
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader != null)
                                        {
                                            while (reader.Read())
                                            {
                                                labTestInfo.UserId = reader["UserId"].ToString();
                                                labTestInfo.DateOfEntry = reader["DateOfEntry"].ToString();
                                                labTestInfo.TestType = reader["TestType"].ToString();
                                                labTestInfo.TestDate = reader["TestDate"].ToString();
                                                labTestInfo.TestResult = reader["TestResult"].ToString();
                                            }
                                        }
                                        return (T)Convert.ChangeType(labTestInfo, typeof(T));
                                    }
                                }
                            }
                        case Constants.getRequestStatus:                            
                            RequestStatus requestStatus = new RequestStatus();
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("GetRequestStatus", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = paramString;
                                    cmd.Prepare();
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader != null)
                                        {
                                            while (reader.Read())
                                            {
                                                requestStatus.UserId = reader["UserId"].ToString();
                                                requestStatus.DateOfEntry = reader["DateOfEntry"].ToString();
                                                requestStatus.ReturnRequestStatus = reader["ReturnRequestStatus"].ToString();
                                            }
                                        }
                                        return (T)Convert.ChangeType(requestStatus, typeof(T));
                                    }
                                }
                            }
                        case Constants.getSymptomsInfo:                            
                            SymptomsInfo symptomsInfo = new SymptomsInfo();
                            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand("GetSymptomsInfo", connection))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = paramString;
                                    cmd.Prepare();
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader != null)
                                        {
                                            while (reader.Read())
                                            {
                                                symptomsInfo.UserId = reader["UserId"].ToString();
                                                symptomsInfo.DateOfEntry = reader["DateOfEntry"].ToString();
                                                symptomsInfo.UserIsExposed = DBNull.Value.Equals(reader["UserIsExposed"]) ? false : (bool)reader["UserIsExposed"];
                                                symptomsInfo.ExposureDate = reader["ExposureDate"].ToString();
                                                symptomsInfo.QuarantineStartDate = reader["QuarantineStartDate"].ToString();
                                                symptomsInfo.QuarantineEndDate = reader["QuarantineEndDate"].ToString();
                                                symptomsInfo.IsSymptomatic = DBNull.Value.Equals(reader["IsSymptomatic"]) ? false : (bool)reader["IsSymptomatic"];
                                                symptomsInfo.SymptomFever = DBNull.Value.Equals(reader["SymptomFever"]) ? false : (bool)reader["SymptomFever"];
                                                symptomsInfo.SymptomCough = DBNull.Value.Equals(reader["SymptomCough"]) ? false : (bool)reader["SymptomCough"];
                                                symptomsInfo.SymptomShortnessOfBreath = DBNull.Value.Equals(reader["SymptomShortnessOfBreath"]) ? false : (bool)reader["SymptomShortnessOfBreath"];
                                                symptomsInfo.SymptomChills = DBNull.Value.Equals(reader["SymptomChills"]) ? false : (bool)reader["SymptomChills"];
                                                symptomsInfo.SymptomMusclePain = DBNull.Value.Equals(reader["SymptomMusclePain"]) ? false : (bool)reader["SymptomMusclePain"];
                                                symptomsInfo.SymptomSoreThroat = DBNull.Value.Equals(reader["SymptomSoreThroat"]) ? false : (bool)reader["SymptomSoreThroat"];
                                                symptomsInfo.SymptomLossOfSmellTaste = DBNull.Value.Equals(reader["SymptomLossOfSmellTaste"]) ? false : (bool)reader["SymptomLossOfSmellTaste"];
                                                symptomsInfo.SymptomVomiting = DBNull.Value.Equals(reader["SymptomVomiting"]) ? false : (bool)reader["SymptomVomiting"];
                                                symptomsInfo.SymptomDiarrhea = DBNull.Value.Equals(reader["SymptomDiarrhea"]) ? false : (bool)reader["SymptomDiarrhea"];
                                                symptomsInfo.Temperature = DBNull.Value.Equals(reader["Temperature"]) ? 0 : (decimal)reader["Temperature"];
                                                symptomsInfo.UserIsSymptomatic = DBNull.Value.Equals(reader["UserIsSymptomatic"]) ? false : (bool)reader["UserIsSymptomatic"];
                                                symptomsInfo.ClearToWorkToday = DBNull.Value.Equals(reader["ClearToWorkToday"]) ? false : (bool)reader["ClearToWorkToday"];
                                                symptomsInfo.GUID = reader["GUID"].ToString();
                                            }
                                        }
                                        return (T)Convert.ChangeType(symptomsInfo, typeof(T));
                                    }
                                }
                            }
                        default:
                            errorMsg = "Error in getting data from database";
                            throw new ArgumentNullException(errorMsg);                            
                    }
                }
                errorMsg = "Error: SQL Connection Parameters not found in Configuration";
                throw new ArgumentNullException(errorMsg);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool GetTeamsAddress(List<TeamsAddressQuarantineInfo> teamsAddressQuarantineInfoCollector)
        {            
            try
            {     
                if(teamsAddressQuarantineInfoCollector == null)
                {
                    throw new ArgumentNullException(nameof(teamsAddressQuarantineInfoCollector)); 
                }
                string sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);
                if (!String.IsNullOrEmpty(sqlConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("GetAllTeamsAddress", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;                            
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    while (reader.Read())
                                    {
                                        TeamsAddressQuarantineInfo teamsAddressQuarantineInfo = new TeamsAddressQuarantineInfo();
                                        teamsAddressQuarantineInfo.UserId = reader["UserId"].ToString();
                                        teamsAddressQuarantineInfo.TeamsAddress = reader["TeamsAddress"].ToString();
                                        teamsAddressQuarantineInfoCollector.Add(teamsAddressQuarantineInfo);
                                    }
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                }
                string errorMsg = "Error: SQL Connection Parameters not found in Configuration";
                throw new ArgumentNullException(errorMsg);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool GetUserContactInfo(List<UserContactInfo> userContactInfoCollector)
        {            
            try
            {
                if(userContactInfoCollector == null)
                {
                    throw new ArgumentNullException(nameof(userContactInfoCollector));
                }
                string sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString", EnvironmentVariableTarget.Process);
                if (!String.IsNullOrEmpty(sqlConnectionString))
                {
                    using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("GetAllUsersContactInfo", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    while (reader.Read())
                                    {
                                        UserContactInfo userContactInfo = new UserContactInfo();
                                        userContactInfo.UserId = reader["UserId"].ToString();
                                        userContactInfo.FullName = reader["FullName"].ToString();
                                        userContactInfo.EmailAddress = reader["EmailAddress"].ToString();
                                        userContactInfo.MobilePhone = reader["MobileNumber"].ToString();
                                        userContactInfoCollector.Add(userContactInfo);
                                    }
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                }
                string errorMsg = "Error: SQL Connection Parameters not found in Configuration";
                throw new ArgumentNullException(errorMsg);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                throw new ArgumentNullException(argNullEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
