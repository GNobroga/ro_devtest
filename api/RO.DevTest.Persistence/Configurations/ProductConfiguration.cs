using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Configurations;

public class ProductConfiguration() : BaseConfiguration<Product>(TABLE_NAME) {
    private const string TABLE_NAME = "Products";
}