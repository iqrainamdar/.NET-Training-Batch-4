using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using StudentManagement.Entities;
using StudentManagement.Model;

namespace StudentManagement.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class StudentController : Controller
    {

        public Container Container;
        public StudentController()
        {
            Container = GetContainer();
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "";
        public string DatabaseName = "Batch4";
        public string ContainerName = "Student";

        //[HttpPost]
        //public async Task<IActionResult> AddStudent(StudentModel studentModel)
        //{    
        //    var student = await Container.CreateItemAsync(studentModel);
        //    return Ok(student);
        //}

        ////[HttpGet]
        ////public async Task<StudentModel> GetStudentById(string id)
        ////{
        ////    var student = Container.GetItemLinqQueryable<StudentModel>(true).Where(q => q.Id == id).FirstOrDefault();
        ////    return student;
        ////}

        //[HttpGet]
        //public async Task<List<StudentModel>> GetAllStudents()
        //{
        //    //query to get all records
        //    var students = Container.GetItemLinqQueryable<StudentModel>(true).ToList();
        //    //return the result
        //    return students;
          
        //}


        [HttpPost]
        public async Task<StudentModel> AddStudentEntity(StudentModel studentModel)
        {
            //1. create obj of entity and mapp all the fields from model to entity
            StudentEntity student = new StudentEntity();
            student.RollNo = studentModel.RollNo;
            student.StudentName = studentModel.StudentName;
            student.PhoneNumber = studentModel.PhoneNumber;
            student.Age = studentModel.Age;

            //2. Assign values to madatory fields
            student.Id = Guid.NewGuid().ToString();
            student.UId = student.Id;
            student.DocumentType = "student";
            student.CreatedBy = "Iqra";
            student.CreatedOn = DateTime.Now;
            student.UpdatedBy = "";
            student.UpdatedOn = DateTime.Now;
            student.Version = 1;
            student.Active = true;
            student.Archived = false;

            //3. Add the data to database
            StudentEntity response = await Container.CreateItemAsync(student);

            //4. return the model
            StudentModel responseModel = new StudentModel();
            responseModel.RollNo = response.RollNo;
            responseModel.StudentName = response.StudentName;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Age = response.Age;
            return responseModel;

        }



        [HttpGet]
        public async Task<List<StudentModel>> GetAllStudents()
        {
            //1. fetch the records 
            var students = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "student").ToList();

            //2. map the fields to model
            List<StudentModel> studentModels = new List<StudentModel>();

            foreach (var student in students)
            {
                StudentModel model = new StudentModel();
                model.UId = student.UId;
                model.RollNo = student.RollNo;
                model.StudentName = student.StudentName;
                model.PhoneNumber = student.PhoneNumber;
                model.Age = student.Age;

                studentModels.Add(model);
            }

            //3. Return
            return studentModels;
        }

        [HttpGet]
        public async Task<StudentModel> GetStudentByUId(string UId)
        {
            //1. get record
            var student = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            //2. map the fields
            StudentModel studentModel = new StudentModel();
            studentModel.UId = student.UId;
            studentModel.RollNo = student.RollNo;
            studentModel.StudentName = student.StudentName;
            studentModel.PhoneNumber = student.PhoneNumber;
            studentModel.Age = student.Age;
            

            //3. return
            return studentModel;
        }


        [HttpPost]
        public async Task<StudentModel> UpdateStudent(StudentModel student)
        {
            //1. get the existing record by UId
            var existingStudent = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.UId == student.UId && q.Active ==true && q.Archived == false).FirstOrDefault();

            //2. Replace the records
            existingStudent.Archived = true;
            existingStudent.Active = false;
            await Container.ReplaceItemAsync(existingStudent, existingStudent.Id);

            //3. Assign the values for mandatory fields

            existingStudent.Id = Guid.NewGuid().ToString();
            existingStudent.UpdatedBy = "Iqra";
            existingStudent.UpdatedOn = DateTime.Now;
            existingStudent.Version = existingStudent.Version + 1;
            existingStudent.Active = true;
            existingStudent.Archived = false;

            //4. Assign the values to the fields which we will get from request obj
            existingStudent.RollNo = student.RollNo;
            existingStudent.StudentName = student.StudentName;
            existingStudent.PhoneNumber = student.PhoneNumber;
            existingStudent.Age = student.Age;

            //5. Add the data to database

            existingStudent = await Container.CreateItemAsync(existingStudent);

            //6. Return

            StudentModel reponse = new StudentModel();
            reponse.UId = existingStudent.UId;
            reponse.StudentName = existingStudent.StudentName;
            reponse.PhoneNumber = existingStudent.PhoneNumber;
           reponse.RollNo = existingStudent.RollNo;
            reponse.Age = existingStudent.Age;
            return reponse;

        }








        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}


//Update Reocrd
// Delete record
//getall
//What are entities
//What are DTOs
//Mapping