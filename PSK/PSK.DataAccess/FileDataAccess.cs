using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Persistence;
using File = PSK.Domain.File;

namespace PSK.DataAccess
{
    public class FileDataAccess : IFileDataAccess
    {
        private readonly IDataContext _context;

        public FileDataAccess(IDataContext context)
        {
            _context = context;
        }

        public async Task<File> Add(IFormFile formFile, string path, TripEmployee te)
        {
            string fileName = Path.GetFileName(formFile.FileName);
            string filePath = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            var addedFile = await _context.Files.AddAsync(new File
            {
                Name = fileName,
                Size = formFile.Length,
                TripEmployee = te
            });
            await _context.SaveChangesAsync();
            return addedFile.Entity;
        }
    }
}