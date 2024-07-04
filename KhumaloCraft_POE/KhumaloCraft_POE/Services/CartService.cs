using KhumaloCraft_Part2.Models;
using System.Collections.Generic;
using System.Linq;

namespace KhumaloCraft_Part2.Services
{
    public class CartService
    {
        // Private list to hold cart items
        private readonly List<CartItem> _cartItems;

        // Constructor initializes the cart items list
        public CartService()
        {
            _cartItems = new List<CartItem>();
        }

        // Method to add an item to the cart
        public void AddToCart(CartItem item)
        {
            // Check if the item already exists in the cart
            var existingItem = _cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                // If it exists, increase the quantity
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                // If it doesn't exist, add the new item to the cart
                _cartItems.Add(item);
            }
        }

        // Method to retrieve all items in the cart
        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        // Method to remove an item from the cart based on product ID
        public void RemoveFromCart(int productId)
        {
            // Find the item to remove
            var itemToRemove = _cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (itemToRemove != null)
            {
                // Remove the item if found
                _cartItems.Remove(itemToRemove);
            }
        }

        // Method to update the quantity of a specific item in the cart
        public void UpdateCartQuantity(int productId, int quantity)
        {
            // Find the item to update
            var itemToUpdate = _cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (itemToUpdate != null)
            {
                // Update the quantity if the item is found
                itemToUpdate.Quantity = quantity;
            }
        }

        // Method to clear all items from the cart
        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}