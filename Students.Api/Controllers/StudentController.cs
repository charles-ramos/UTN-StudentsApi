using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Students.Api.ViewModels;
using Students.Common.Contracts;

namespace Students.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IJsonManager<StudentVM> _jsonManager;
        private readonly string _jsonFilePath = @"Data/Students.json";
        private IList<StudentVM> _studentList;

        public StudentController(
            IConfiguration configuration,
            IMemoryCache memoryCache,
            IJsonManager<StudentVM> jsonManager)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _jsonManager = jsonManager;

            var cacheKey = $"{nameof(StudentController)}";

            _studentList = _memoryCache.GetOrCreate(cacheKey, entry => {
                var duration = _configuration.GetChildren().Any(x => x.Key.Equals("MemoryCacheDurationInSeconds")) ? _configuration.GetValue<double>("MemoryCacheDurationInSeconds") : 300;

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(duration);

                return _jsonManager.GetContent(_jsonFilePath);
            })!;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_studentList);
        }

        [HttpGet]
        [Route("{studentId}")]
        public IActionResult GetById(int studentId)
        {
            return Ok(_studentList
                        .Where(x => x.StudentId == studentId)
                        .FirstOrDefault());
        }

        [HttpGet]
        [Route("GetByEmail/{email}")]
        public IActionResult GetById(string email)
        {
            return Ok(_studentList
                        .Where(x => x.Email == email)
                        .FirstOrDefault());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(StudentVM student)
        {
            var studentId = _studentList.Select(x => x.StudentId)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            student.StudentId = studentId + 1;

            _studentList.Add(student);

            return Ok();
        }
    }
}