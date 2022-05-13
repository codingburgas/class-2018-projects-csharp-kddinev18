using BusinessLogicLayer;
using DiabetesTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public static class MainWindowModel
    {
        public static PostInfomation GetPost()
        {
            return new PostInfomation() 
            {
                UserName = PostBusinessLogic.GetPostUser(1),
                Content = PostBusinessLogic.GetPostContent(1),
                Image = PostBusinessLogic.ConvertBytesToImage(PostBusinessLogic.GetPostImage(1))
            };
        }
    }
}
