using BLL.Interfaces.Models.FilesModels;
using BLL.Models.FilesModels;
using MvcApp.Models.FilesModels;
using System;
using System.IO;
using System.Web;
namespace MvcApp.Infrastructure.Mappers
{
    /// <summary>
    /// Provides methods to map instances to other
    /// </summary>
    public static class FileMappers
    {
        #region Public methods        
        /// <summary>
        /// Map to the BLL file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Bll file</returns>
        public static IBllFile ToBllFile(this HttpPostedFileBase file)
        {
            return new BllFile()
            {
                ContentType = file.ContentType,
                Name = file.FileName,
                Size = file.ContentLength
            };
        }

        /// <summary>
        /// Map component to the component view model.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>ComponentViewModel instance</returns>
        public static ComponentViewModel ToComponentViewModel(this IBllComponent component)
        {
            return new ComponentViewModel()
            {
                Id = component.Id,
                DateUploaded = component.DateUploaded,
                Name = component.Name,
                Size = component.Size,
                ContentType = component.ContentType
            };
        } 
        #endregion
    }
}
