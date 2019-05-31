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
                TripEmployee = te,
                FullName = filePath
            });
            await _context.SaveChangesAsync();
            return addedFile.Entity;
        }

        public async Task<IList<File>> GetForTE(Guid tripEmployeeId)
        {
            return await _context.Files.Where(f => f.TripEmployee.Id == tripEmployeeId).ToListAsync();
        }

        public async Task<File> Get(Guid id)
        {
            return await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteForTE(Guid tripEmployeeId)
        {
            var toDelete = await GetForTE(tripEmployeeId);
            foreach (var file in toDelete)
            {
                _context.Files.Remove(file);
                System.IO.File.Delete(file.FullName);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            File file = await Get(id);
            string path = file.FullName;
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            try
                {
                System.IO.File.Delete(path);
                }
            catch (Exception e) when (e is DirectoryNotFoundException || e is UnauthorizedAccessException)
                {
                }
        }
    }
}