using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalProduct : IProduct
    {
        string ProductPath = "Product";
        static XElement config = XmlTools.LoadConfig();
        static DO.Product? createProductfromXElement(XElement s)
        {
            return new DO.Product
            {
                ID = s.ToIntNullable("ID") ?? throw new FormatException("ProductID"),
                Name = (string?)s.Element("Name")!.Value,
                Category = s.ToEnumNullable<DO.Category>("Category"),
                Price = (double)s.Element("Price")!,
                InStock = (int)s.Element("InStock")!
            };
        }
        public int Add(Product entity)
        {
            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);
            if (entity.ID == 0)
            {
                entity.ID = int.Parse(config.Element("ID")!.Value) + 1;
                XmlTools.SaveConfigXElement("ID", entity.ID);
            }
            XElement? prod = (from st in product_root.Elements()
                              where st.ToIntNullable("ID") == entity.ID
                              select st).FirstOrDefault();
            if (prod != null)
                throw new Exception("ID already exist");
            product_root.Add(new XElement("Product",
                                       new XElement("ID", entity.ID),
                                       new XElement("Name", entity.Name),
                                       new XElement("Category", entity.Category),
                                       new XElement("Price", entity.Price),
                                       new XElement("InStock", entity.InStock)
                                       ));
            XmlTools.SaveListToXMLElement(product_root, ProductPath);
            return entity.ID;
        }

        public void Delete(Product entity)
        {
            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);

            XElement? prod = (from st in product_root.Elements()
                              where (int?)st.Element("ProductID") == entity.ID
                              select st).FirstOrDefault() ?? throw new Exception("Missing ID");
            prod.Remove();  

            XmlTools.SaveListToXMLElement(product_root, ProductPath);
        }

        public Product? Get(int entity)
        {
            List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(ProductPath);

            var product = ListProduct.FirstOrDefault(x => x?.ID == entity);

            if (product == null)
                throw new DO.NotFoundException("Product Id not found");
            else
                return (DO.Product)product;
        }

        public Product? Get(Func<Product?, bool>? selector)
        {
            if (selector == null)
                throw new Exception("missing function");

            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);
            return ((from p in product_root.Elements()
                     where selector(p.ConvertProduct_Xml_to_D0())
                     select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? selector = null)
        {
            XElement? product_root = XmlTools.LoadListFromXMLElement(ProductPath);
            if (selector != null)
            {
                return from s in product_root.Elements()
                       let prod = createProductfromXElement(s)
                       where selector(prod)
                       select prod;
            }
            else
            {
                return from s in product_root.Elements()
                       select createProductfromXElement(s);
            }
        }

        public void Update(Product entity)
        {
            List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(ProductPath);

            bool found = false;
            var foundProduct = ListProduct.FirstOrDefault(prod => prod?.ID == entity.ID);
            if (foundProduct != null)
            {
                found = true;
                int index = ListProduct.IndexOf(foundProduct);
                ListProduct[index] = entity;
            }
            if (found == false)
                throw new DO.NotFoundException("order item id not found");
            XmlTools.SaveListToXMLSerializer(ListProduct, ProductPath);
        }
    }
}
