using IMG_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMG_DataModel
{
    public class VisaCardDAO:IVisaCardDAO
    {

     

        public int SaveNewMember(VisaCard visaCard)
        {
            int noOfITemsSaved;
            dbConnection conn = new dbConnection();
            SqlCommand myCommand = new SqlCommand();
            SqlCommand myCmdInsertIntoAgentAccntInfo = new SqlCommand();

            myCommand.CommandText = "INSERT INTO AgentVisaCardInfo (accountNo, agentCode, lastName,firstName,middleName,addr1,phone1,phone2,sex,birthday,titleCode,motherMaidenName,birthPlace,maritalStatus,spouseName,permanentAddr1,idType,idNo,TIN,SSS,occupationDescription,occupationCode,currentEmployer,nationality,sourceOfFunds)"+
            "VALUES(@accountNo,@agentCode,@lastName,@firstName,@middleName,@addr1,@phone1,@phone2,@sex,@birthday,@titleCode,@motherMaidenName,@birthPlace,@maritalStatus,@spouseName,@permanentAddr1,@idType,@idNo,@TIN,@SSS,@occupationDescription,@occupationCode,@currentEmployer,@nationality,@sourceOfFunds)";
            myCommand.Parameters.AddWithValue("@accountNo", visaCard.Account_No.Trim());
            myCommand.Parameters.AddWithValue("@agentCode", visaCard.AgentCode.Trim());
            myCommand.Parameters.AddWithValue("@lastName", visaCard.LastName.Trim());
            myCommand.Parameters.AddWithValue("@firstName", visaCard.FirstName.Trim());
            myCommand.Parameters.AddWithValue("@middleName", visaCard.MiddleName.Trim());
            myCommand.Parameters.AddWithValue("@addr1", visaCard.Addr1.Trim());
            myCommand.Parameters.AddWithValue("@phone1", visaCard.Phone1.Trim());
            myCommand.Parameters.AddWithValue("@phone2", visaCard.Phone2.Trim());
            myCommand.Parameters.AddWithValue("@sex", visaCard.Sex.Trim());
            myCommand.Parameters.AddWithValue("@birthday", visaCard.Birthday.Trim());
            myCommand.Parameters.AddWithValue("@titleCode", visaCard.TITLE_CODE.Trim());
            myCommand.Parameters.AddWithValue("@motherMaidenName", visaCard.MotherMaidenName.Trim());
            myCommand.Parameters.AddWithValue("@birthPlace", visaCard.Birthplace.Trim());
            myCommand.Parameters.AddWithValue("@maritalStatus", visaCard.MaritalStatus.Trim());
            myCommand.Parameters.AddWithValue("@spouseName", visaCard.SpouseName.Trim());
            myCommand.Parameters.AddWithValue("@permanentAddr1", visaCard.PERMANENT_ADD1.Trim());
            myCommand.Parameters.AddWithValue("@idType", visaCard.ID_TYPE.Trim());
            myCommand.Parameters.AddWithValue("@idNo", visaCard.ID_NO.Trim());
            myCommand.Parameters.AddWithValue("@TIN", visaCard.TIN.Trim());
            myCommand.Parameters.AddWithValue("@SSS", visaCard.SSS.Trim());
            myCommand.Parameters.AddWithValue("@occupationDescription", visaCard.OccupationDesc.Trim());
            myCommand.Parameters.AddWithValue("@occupationCode", visaCard.OccupationCode.Trim());
            myCommand.Parameters.AddWithValue("@currentEmployer", visaCard.CurrentEmployer.Trim());
            myCommand.Parameters.AddWithValue("@nationality", visaCard.Nationality.Trim());
            myCommand.Parameters.AddWithValue("@sourceOfFunds", visaCard.SourceOfFunds.Trim());
            myCommand.CommandType = CommandType.Text;

            myCmdInsertIntoAgentAccntInfo.CommandText = "INSERT INTO AgentAccountInfo (accountNo, agentCode) VALUES (@param1, @param2)";
            myCmdInsertIntoAgentAccntInfo.Parameters.AddWithValue("@param1", visaCard.Account_No);
            myCmdInsertIntoAgentAccntInfo.Parameters.AddWithValue("@param2", visaCard.AgentCode);
            myCmdInsertIntoAgentAccntInfo.CommandType = CommandType.Text;
           
            try
            {
               myCommand.Connection = conn.openConnection();
               noOfITemsSaved = myCommand.ExecuteNonQuery();
               myCmdInsertIntoAgentAccntInfo.Connection = conn.openConnection();
               myCmdInsertIntoAgentAccntInfo.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving!", ex);
            }
            finally
            {
                conn.closeConnection();
            }
            return noOfITemsSaved;
        }

        public List<VisaCard> CheckIfAgentCodeAlreadyExists(List<VisaCard> visaCardList)
        {
            List<VisaCard> listOfExistingAgents = new List<VisaCard>();
            for (int i = 0; i < visaCardList.Count; i++)
            {
                dbConnection conn = new dbConnection();
                SqlCommand myCommand = new SqlCommand();
                SqlDataReader reader;
                myCommand.CommandText = "SELECT * FROM AgentVisaCardInfo WHERE AgentCode = @param";
                myCommand.Parameters.AddWithValue("@param", visaCardList[i].AgentCode);
                myCommand.CommandType = CommandType.Text;

                try
                {
                    VisaCard visaCard = new VisaCard();
                    myCommand.Connection = conn.openConnection();
                    reader = myCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        visaCard.AgentCode = reader[1].ToString();
                        visaCard.LastName = reader[2].ToString();
                        visaCard.FirstName = reader[3].ToString();
                        visaCard.MiddleName = reader[4].ToString();

                        listOfExistingAgents.Add(visaCard);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Error!");
                }
                finally
                {
                    conn.closeConnection();
                }
            }

            return listOfExistingAgents;
        }


        public int RegisterUser(Users user)
        {

            int roleId = GetRoleID(user);

            int noOfITemsSaved =0;
            dbConnection conn = new dbConnection();
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = "INSERT INTO Users (userName, password, fullName, department,roleId) VALUES ( @param1, @param2, @param3, @param4, @param5)";
            
            myCommand.Parameters.AddWithValue("@param1", user.userName);
            myCommand.Parameters.AddWithValue("@param2", user.password);
            myCommand.Parameters.AddWithValue("@param3", user.fullName);
            myCommand.Parameters.AddWithValue("@param4", user.department);
            myCommand.Parameters.AddWithValue("@param5", roleId);
            myCommand.CommandType = CommandType.Text;

            try
            {
                myCommand.Connection = conn.openConnection();
                noOfITemsSaved = myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error!", ex);
            }
            finally
            {
                conn.closeConnection();
            }
            return noOfITemsSaved;
        }

        private int GetRoleID(Users user)
        {
            SqlDataReader reader;
            dbConnection conn = new dbConnection();
            SqlCommand mymdGetRole = new SqlCommand();
            mymdGetRole.CommandText = "SELECT * FROM Roles WHERE roleName = @param1";
            mymdGetRole.Parameters.AddWithValue("@param1", user.department);
            mymdGetRole.CommandType = CommandType.Text;
            string roleDefinition;
            int roleId=0;
            try
            {

                mymdGetRole.Connection = conn.openConnection();
                reader = mymdGetRole.ExecuteReader();
                while (reader.Read())
                {
                    roleId = (int)reader[0];
                    roleDefinition = reader[1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error!", ex);
            }
            finally
            {
                conn.closeConnection();
            }
            return roleId;
        }


        public Users ValidateUser(string userName, string password)
        {
            dbConnection conn = new dbConnection();
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader reader;
            myCommand.CommandText = "SELECT * FROM Users WHERE userName = @param1 AND password = @param2" ;
            myCommand.Parameters.AddWithValue("@param1", userName);
            myCommand.Parameters.AddWithValue("@param2", password);
            myCommand.CommandType = CommandType.Text;
            Users user = new Users();
            try
            {
              
                myCommand.Connection = conn.openConnection();
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                   user.userName = reader[1].ToString();
                   user.password = reader[2].ToString();
                   user.fullName = reader[3].ToString();
                   user.department = reader[4].ToString();
                  // user.roleId = reader.GetInt32(reader.GetOrdinal("roleId")); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error!", ex);
            }
            finally
            {
                conn.closeConnection();
            }
            return user;
        }
    }
}
