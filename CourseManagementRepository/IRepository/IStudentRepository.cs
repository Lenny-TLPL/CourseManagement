﻿using CourseManangementModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        public Student GetStudentByUserBasicId(int userBasicId);
    }
}
