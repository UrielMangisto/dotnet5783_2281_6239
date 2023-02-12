using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        static DO.Product? createProductfromXElement(XElement s)
        {
            return new DO.Product
            {
                productID = s.ToIntNullable("productID") ?? throw new FormatException("ProductID"),
                Name = (string?)s.Element("Name")!.Value,
                Category = s.ToEnumNullable<DO.Category>("Category"),
                Price = (double)s.Element("Price")!,
                InStock = (int)s.Element("InStock")!
            };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Product entity)
        {
            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);
            if (entity.productID == 0)
            {
                entity.productID = int.Parse(config.Element("productID")!.Value) + 1;
                XmlTools.SaveConfigXElement("productID", entity.productID);
            }
            XElement? prod = (from st in product_root.Elements()
                              where st.ToIntNullable("productID") == entity.productID
                              select st).FirstOrDefault();
            if (prod != null)
                throw new Exception("ID already exist");
            product_root.Add(new XElement("Product",
                                       new XElement("productID", entity.productID),
                                       new XElement("Name", entity.Name),
                                       new XElement("Category", entity.Category),
                                       new XElement("Price", entity.Price),
                                       new XElement("InStock", entity.InStock)
                                       ));
            XmlTools.SaveConfigXElement("productID", entity.productID);
            XmlTools.SaveListToXMLElement(product_root, ProductPath);
            return entity.productID;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(Product entity)
        {
            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);

            XElement? prod = (from st in product_root.Elements()
                              where (int?)st.Element("ProductID") == entity.productID
                              select st).FirstOrDefault() ?? throw new Exception("Missing ID");
            prod.Remove();  

            XmlTools.SaveListToXMLElement(product_root, ProductPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Product? Get(int entity)
        {
            List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(ProductPath);

            var product = ListProduct.FirstOrDefault(x => x?.productID == entity);

            if (product == null)
                throw new DO.NotFoundException("Product Id not found");
            else
                return (DO.Product)product;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Product? Get(Func<Product?, bool>? selector)
        {
            if (selector == null)
                throw new Exception("missing function");

            XElement product_root = XmlTools.LoadListFromXMLElement(ProductPath);
            return ((from p in product_root.Elements()
                     where selector(p.ConvertProduct_Xml_to_D0())
                     select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Product entity)
        {
            List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(ProductPath);

            bool found = false;
            var foundProduct = ListProduct.FirstOrDefault(prod => prod?.productID == entity.productID);
            if (foundProduct != null)
            {
                found = true;
                int index = ListProduct.IndexOf(foundProduct);
                ListProduct[index] = entity;
            }
            if (found == false)
                throw new DO.NotFoundException("Order item id not found");
            XmlTools.SaveListToXMLSerializer(ListProduct, ProductPath);
        }
    }
}
