using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Models;
using Application.DataAccess;
using Application.DataAcces;

namespace Application.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        private static List<Customer> customers;

        //public object DataAccess { get; private set; }

        #region -- Los datos no se encuentran guardados más que en memoria --

        static CustomerRepository()
        {
            // Carga de 100 clientes de forma predeterminada
            customers = new List<Customer>();

            //for (int i = 0; i < 20; i++)
            //{
            //    var customer = new Customer();
            //    customer.Id = i + 1;
            //    customer.Name = "CustomerName" + customer.Id;
            //    customer.LastName = "CustomerLastname" + customer.Id;
            //    customer.Age = 20 + (int)customer.Id;

            //    customers.Add(customer);
            //}
        }

        #endregion

        public override void Create(Customer entity)
        {
            try
            {
                DataAccess.DataAccess.InsertCustomer(entity);
            }
            catch (Exception ex)
            {
                throw new TechnicalException(
                    string.Format("No se pudo crear el cliente \"{0} {1}\".", entity.Name, entity.LastName),
                    ex);
            }

        }

        public override List<Customer> GetAll()
        {
            return DataAccess.DataAccess.GetCustomers();
        }
        public List<Customer> GetAll(string path)
        {
            CustomerSerializer customerSerializer = new CustomerSerializer();
            customers = customerSerializer.Read(path);
            return customers;
        }

        public override Customer GetById(int entityId)
        {
            try
            {
                Customer customer = DataAccess.DataAccess.GetById(entityId);
                if (!(customer is null))
                {
                    return customer;
                }
                throw new Exception("No se encontró ese ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Remove(Customer entity)
        {
            // TODO: implementar
            try
            {
                DataAccess.DataAccess.DeleteCustomer(entity.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Update(Customer entity)
        {
            // TODO: implementar
            try
            {
                DataAccess.DataAccess.UpdateCustomer(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer> LoadFromFile(string path)
        {

            CustomerSerializer customerSerializer = new CustomerSerializer();
            customers.AddRange(customerSerializer.Read(path));
            return customers;
        }

        public bool SaveToFile(List<Customer> customers)
        {
            CustomerSerializer customerSerializer = new CustomerSerializer();
            return customerSerializer.Save(customers);
        }

    }
}
