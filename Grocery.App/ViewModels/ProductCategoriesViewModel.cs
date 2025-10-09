using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    public partial class ProductCategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<ProductCategory> ProductCategories { get; set; } = [];
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        [ObservableProperty]
        Category category = new(0, "None");

        public ProductCategoriesViewModel(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            Load(category.Id);
        }

        private void Load(int categoryId)
        {
            ProductCategories.Clear();
            foreach (var item in _productCategoryService.GetAllOnCategoryId(categoryId))
            {
                ProductCategories.Add(item);
            }
        }

        partial void OnCategoryChanged(Category value)
        {
            Title = value.Name;
            Load(value.Id);
        }

        [RelayCommand]
        public void IncreaseStock(int productId)
        {
            ProductCategory? item = ProductCategories.FirstOrDefault(x => x.ProductId == productId);
            if (item == null) return;
            item.Product.Stock++;
            _productService.Update(item.Product);
        }

        [RelayCommand]
        public void DecreaseStock(int productId)
        {
            ProductCategory? item = ProductCategories.FirstOrDefault(x => x.ProductId == productId);
            if (item == null || item.Product.Stock <= 0) return;
            item.Product.Stock--;
            _productService.Update(item.Product);
        }
    }
}