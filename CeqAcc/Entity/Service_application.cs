//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CeqAcc.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service_application
    {
        public int code_request { get; set; }
        public string date_start { get; set; }
        public System.TimeSpan time_start { get; set; }
        public System.DateTime date_finish { get; set; }
        public Nullable<int> id_driver { get; set; }
        public System.TimeSpan time_finish { get; set; }
        public int admin_id { get; set; }
        public int code_service { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Request Request { get; set; }
        public virtual Value_of_taximeter Value_of_taximeter { get; set; }
    }
}