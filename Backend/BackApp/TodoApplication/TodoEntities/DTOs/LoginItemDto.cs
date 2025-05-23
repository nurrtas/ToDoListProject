using System.ComponentModel.DataAnnotations;

namespace TodoList.BackApp.TodoApplication.TodoEntities.ItemDtos

{

    public class LoginItemDto

    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        public string Password { get; set; }
    }
}