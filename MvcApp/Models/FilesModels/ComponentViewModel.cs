using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models.FilesModels
{
    /// <summary>
    /// Model of components stored at user drive
    /// </summary>
    public class ComponentViewModel
    {
        #region Properties        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date uploaded.
        /// </summary>
        /// <value>
        /// The date uploaded.
        /// </value>
        public DateTime DateUploaded { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; } 
        #endregion
    }
}