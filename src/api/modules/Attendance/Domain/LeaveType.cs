using System.Globalization;
using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;

namespace FSH.Starter.WebApi.Attendance.Domain;
public class LeaveType : AuditableEntity, IAggregateRoot
{
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            SetIdentifierFromName();
        }
    }

    public string Identifier { get; private set; } = string.Empty;

    public double Value { get; set; }

    public ICollection<Leave> Leaves { get; } = new List<Leave>();

    private void SetIdentifierFromName()
    {
        if (!string.IsNullOrEmpty(_name))
        {
            Identifier = _name.ToUpper(CultureInfo.CurrentCulture).Replace(" ", "_", StringComparison.OrdinalIgnoreCase);
        }
    }
}
