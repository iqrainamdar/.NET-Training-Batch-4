using Newtonsoft.Json;

namespace StudentManagement.Model
{
    public class StudentModel
    {

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "rollNo", NullValueHandling = NullValueHandling.Ignore)]
        public string RollNo { get; set; }

        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentName { get; set; }

        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Ignore)]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }
    }

    //1. camelCase
    //2. PascalCase
}

//id --> different for each record , entity

//uid -->  cant change when we update the data , record

//dtype --> Partition key 

//version --> it will incremented for each uopdate

//updatedon --> datetime of updation
//updatedBy --> who is updating the record
//createdOn --> datetime of data created
//createdBy --> who is creating data
//active --> data is present
//archieve

//id = 10
//uid = 1000
//roll no = 1
//name = abc
//active =false
//archieved =true

//id= 20
//uid =1000
//roll no = 2
//name = abc
//active = true
//archieved = false
