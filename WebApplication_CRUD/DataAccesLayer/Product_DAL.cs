using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_CRUD.Models;

namespace WebApplication_CRUD.DataAccesLayer
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        private object sqlDbType;

        //get all products

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllProducts_SP";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();


                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });

                }
            }
            return productList;
        }

        //Insert Product

        public bool InsertProduct(Product product)
        {
            int id = 0;
            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("InsertProducts_SP",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Price",product.Price);
                command.Parameters.AddWithValue("Qty", product.Qty);
                command.Parameters.AddWithValue("Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close(); 
            }
            if (id > 0) 
            { 
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        //Get Products by id
        public List<Product> GetProductsByID( int ProductID)
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetProductByID_SP";
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();


                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });

                }
            }
            return productList;
        }

        //Update Product

        public bool UpdateProduct(Product product)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("UpdateProducts_SP", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductID", product.ProductID);
                command.Parameters.AddWithValue("ProductName", product.ProductName);
                command.Parameters.AddWithValue("Price", product.Price);
                command.Parameters.AddWithValue("Qty", product.Qty);
                command.Parameters.AddWithValue("Remarks", product.Remarks);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Product

        public string DeleteProduct(int productID)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("DeleteProducts_SP", connection);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@productID",productID);
                command.Parameters.Add("@OutPutMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OutPutMessage"].Value.ToString();
                connection.Close();
            }

            return result;
        }
    }
}