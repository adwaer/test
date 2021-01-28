using System;
using System.Globalization;
using BN.Domain.Features.Products.Models;
using In.Specifications;

namespace BN.Domain.Features.Products
{
    public static class ProductSpecifications
    {
        public static Specification<Product> WithId(int id)
        {
            return new SimpleSpecification<Product>(p => p.Id == id);
        }

        public static Specification<Product> NotDeleted()
        {
            return new SimpleSpecification<Product>(p => p.Deleted == null);
        }

        public static Specification<Product> IsContains(string term)
        {
            if (int.TryParse(term, out var num))
            {
                return new SimpleSpecification<Product>(p =>
                    p.Name.Contains(term) || p.Price == num);
            }

            return new SimpleSpecification<Product>(p => p.Name.Contains(term));
        }
    }
}