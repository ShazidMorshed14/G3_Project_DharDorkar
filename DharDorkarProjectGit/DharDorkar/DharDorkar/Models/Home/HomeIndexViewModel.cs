using DharDorkar.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DharDorkar.DAL;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;

namespace DharDorkar.Models.Home
{
    public class HomeIndexViewModel
    {

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbDharDorkarEntities1 context = new dbDharDorkarEntities1();
        public IPagedList<Tbl_Product> ListOfProducts { get; set; }
        public List<Tbl_Category> ListOfCategory { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pagesize, int? page)
        {

            SqlParameter[] param = new SqlParameter[] {
                   new SqlParameter("@search",search??(object)DBNull.Value)
                   };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pagesize); ;
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return new HomeIndexViewModel
            {
                ListOfProducts = data,
                ListOfCategory = allcategories
            };
        }
    }
}