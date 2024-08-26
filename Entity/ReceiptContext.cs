using Microsoft.EntityFrameworkCore;
using ReceiptProcessorChallenge.Entity.Models;

namespace ReceiptProcessorChallenge.Entity;

public class ReceiptContext(DbContextOptions<ReceiptContext> options) 
    : DbContext(options)
{
    public DbSet<Receipt> Receipts { get; set; } = null!;
    public DbSet<ReceiptItem> ReceiptItems { get; set; } = null!;
}