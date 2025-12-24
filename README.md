# 🛒 Second-Hand Marketplace
**A Secure and Responsive Web Marketplace built with ASP.NET Core**

---

## 👤 Developer Information
- **Developer:** Mobin Mohammadi
- **Date:** December 21, 2025

---

## 🚀 Technical Features & Functionality

### 🔐 Authentication and Role Management
* **Identity System:** Powered by **ASP.NET Core Identity** for secure registration, login, and session management.
* **Admin Panel:** Protected area accessible only to users with the "Admin" role for managing categories.
* **Listing Ownership:** Users have full CRUD (Create, Read, Update, Delete) permissions for their own listings. The system prevents unauthorized users from modifying or deleting items they do not own.

### 🖼 Dynamic Content Handling
* **Image Management:** Supports product image uploads to the local file system (`wwwroot/images`).
* **Smart Editing:** * Preserves the existing image if no new file is selected during updates.
    * Automatically replaces the old image file when a new one is uploaded to keep the storage clean.

### 💾 Database Architecture
* **EF Core:** Utilizes Entity Framework Core for all database operations.
* **Performance Tuning:** Implemented `AsNoTracking()` in the service layer to resolve Entity Tracking conflicts and improve read performance during update operations.


---

## ✨ Additional Enhancements

### 🎨 Modern UI/UX
* **Pill-Style Navigation:** A sleek category navigation bar on the homepage with visual feedback for the active selection.
* **Bootstrap Modal System:** Prevents layout breaking from long text. Detailed product descriptions are displayed in a responsive **Bootstrap Modal** via a "Read More" button.
* **Interactive Cards:** Added hover effects and smooth transitions to product cards for a premium user experience.

### 📞 Smart Contact Buttons
The system intelligently parses seller contact data to provide one-click communication:
* **Email:** Opens default mail clients (Gmail/Outlook) using the `mailto:` protocol.
* **Phone:** Initiates direct calls on mobile devices using the `tel:` protocol.

### 📱 Fully Responsive Design
Optimized for a seamless experience across all devices using **Bootstrap 5**:
* Desktop & Laptop
* Tablets
* Mobile Devices

---

## 🏗 Project Structure

| Layer | Description |
| :--- | :--- |
| **Controllers** | Handles application logic and incoming HTTP requests. |
| **Services** | Business layer where database operations and logic are abstracted. |
| **Models** | Contains Data Models, ViewModels, and DTOs. |
| **Views** | User interface components built with **Razor Pages**. |

---

## 🛠 Tech Stack
* **Framework:** ASP.NET Core 8.0
* **Database:** SQL Server (via Entity Framework Core)
* **Frontend:** Bootstrap 5, Razor Pages, CSS3, JavaScript
* **Security:** ASP.NET Identity
