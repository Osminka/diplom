//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("diplom.Views.RegistrPage.xaml", "Views/RegistrPage.xaml", typeof(global::App1.Views.RegistrPage))]

namespace App1.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\RegistrPage.xaml")]
    public partial class RegistrPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::diplom.MyEntry username;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::diplom.MyEntry email;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::diplom.MyEntry password;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label loginPage;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(RegistrPage));
            username = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::diplom.MyEntry>(this, "username");
            email = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::diplom.MyEntry>(this, "email");
            password = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::diplom.MyEntry>(this, "password");
            loginPage = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "loginPage");
        }
    }
}
