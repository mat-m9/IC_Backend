namespace IC_Backend
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Base = Root;

        public static class Identity
        {
            public const string Login = Base + "/Identity/login";
            public const string Register = Base + "/Identity/register";
            public const string Refresh = Base + "/Identity/refresh";
            public const string Change = Base + "/Identity/changePassword";
        }

        public static class Producto
        {
            public const string IdUsuario = "/api/Producto/IdUsuario";
            public const string Descripcion = "/api/Producto/Desc";
            public const string Busqueda = "/api/Producto/Buscar";
        }

        public static class Compra
        {
            public const string Comprador = "/api/Compra/IdComprador";
            public const string Vendedor = "/api/Compra/IdVendedor";
        }
    }
}
