using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
namespace CollegeKids2._0.Models;
public class ReplaceTextEventHandler : IEventListener
{
    private readonly string _nombreAlumno;
    private readonly string _grado;
    private readonly string _dniAlumno;
    private readonly string _fecha;

    public ReplaceTextEventHandler(string nombreAlumno, string grado, string dniAlumno, string fecha)
    {
        _nombreAlumno = nombreAlumno;
        _grado = grado;
        _dniAlumno = dniAlumno;
        _fecha = fecha;
    }

    public void EventOccurred(IEventData data, EventType type)
    {
        if (type == EventType.RENDER_TEXT)
        {
            var renderInfo = (TextRenderInfo)data;
            string text = renderInfo.GetText();

            // Reemplaza los placeholders por los valores
            if (text.Contains("{{NombreAlumno}}"))
                ReplaceText(renderInfo, _nombreAlumno);
            if (text.Contains("{{Grado}}"))
                ReplaceText(renderInfo, _grado);
            if (text.Contains("{{DniAlumno}}"))
                ReplaceText(renderInfo, _dniAlumno);
            if (text.Contains("{{Fecha}}"))
                ReplaceText(renderInfo, _fecha);
        }
    }

    public ICollection<EventType> GetSupportedEvents()
    {
        throw new NotImplementedException();
    }

    public ICollection<EventType> GetSupportedEventTypes()
    {
        return new HashSet<EventType> { EventType.RENDER_TEXT };
    }

    private void ReplaceText(TextRenderInfo renderInfo, string newText)
    {
        // Aquí se puede personalizar el reemplazo de texto
        // renderInfo.SetText(newText); // Este método puede variar según la versión de iText
    }
}

