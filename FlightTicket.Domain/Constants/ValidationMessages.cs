using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Constants;

public static class ValidationMessages
{
    public const string NotEmpty = "Lütfen boş bırakmayınız";
    public const string ValidDate = "Lütfen geçerli bir tarih giriniz";
    public const string RequiredSelect = "Lütfen bir seçim yapınız";
    public const string ValidateGuid = "Lütfen geçerli bir Id giriniz";

}
