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

            myCommand.CommandText = "INSERT INTO AgentVisaCardInfo (lastName,firstName,middleName,agentCode) VALUES(@param1,@param2,@param3,@param4)";
            myCommand.Parameters.AddWithValue("@param1", visaCard.LastName);
            myCommand.Parameters.AddWithValue("@param2", visaCard.FirstName);
            myCommand.Parameters.AddWithValue("@param3", visaCard.MiddleName);
            myCommand.Parameters.AddWithValue("@param4", visaCard.AgentCode);
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
                catch (Exception ex)
                {
                    throw new Exception("Error!", ex);
                }
                finally
                {
                    conn.closeConnection();
                }
            }

            return listOfExistingAgents;
        }
    }
}
