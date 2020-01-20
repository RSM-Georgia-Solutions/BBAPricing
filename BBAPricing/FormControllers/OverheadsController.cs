using BBAPricing.Models;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBAPricing.Iterfaces;
using SAPbobsCOM;

namespace BBAPricing.FormControllers
{
    public class OverheadsController
    {

        private List<OverheadsModel> _OverheadsModel;
        private readonly IForm Form;
        private readonly OverheadParamsModel ParamsModel;

        public OverheadsController(IForm form, OverheadParamsModel paramsModel)
        {
            Form = form;
            ParamsModel = paramsModel;
            _OverheadsModel = new List<OverheadsModel>();
        }

        public void CalculateOverheads()
        {
            _OverheadsModel.Clear();
            Recordset rec = (Recordset)DiManager.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rec.DoQuery($@"SELECT * FROM [@RSM_OVERHEAD_C]");

            while (!rec.EoF)
            {
                OverheadsModel model = new OverheadsModel();
                var emp = rec.Fields.Item("U_Component").Value.ToString();
                if (emp == "კაც/საათი")
                {
                    model.Corian = ParamsModel.CorianEmployee * ParamsModel.DailyWorkHours * ParamsModel.WorkingDaysMonthly;
                    model.Neolith = ParamsModel.NeolithEmployee * ParamsModel.DailyWorkHours * ParamsModel.WorkingDaysMonthly;
                    model.Furniture = ParamsModel.FurnitureEmployee * ParamsModel.DailyWorkHours * ParamsModel.WorkingDaysMonthly;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                else if (emp == "Overhead Percent")
                {
                    OverheadsModel manHourModel = _OverheadsModel.First(x => x.ComponentId == "კაც/საათი");
                    model.Corian = manHourModel.Corian / manHourModel.Total * 100;
                    model.Neolith = manHourModel.Neolith / manHourModel.Total * 100;
                    model.Furniture = manHourModel.Furniture / manHourModel.Total * 100;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                else if (emp == "Manufacturing Overhead")
                {
                    OverheadsModel percentModel = _OverheadsModel.First(x => x.ComponentId == "Overhead Percent");
                    model.Corian = percentModel.Corian/100 * ParamsModel.ManufacuringOverhead;
                    model.Neolith = percentModel.Neolith/100 * ParamsModel.ManufacuringOverhead;
                    model.Furniture = percentModel.Furniture/100 * ParamsModel.ManufacuringOverhead;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                else if (emp == "Administrative Overhead")
                {
                    OverheadsModel percentModel = _OverheadsModel.First(x => x.ComponentId == "Overhead Percent");
                    model.Corian = percentModel.Corian / 100 * ParamsModel.AdministrativeOverhead;
                    model.Neolith = percentModel.Neolith / 100 * ParamsModel.AdministrativeOverhead;
                    model.Furniture = percentModel.Furniture / 100 * ParamsModel.AdministrativeOverhead;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                else if (emp == "Manufacturing Overhead 1 კაც/საათზე")
                {
                    OverheadsModel manHourModel = _OverheadsModel.First(x => x.ComponentId == "კაც/საათი");
                    OverheadsModel manufacturingModel = _OverheadsModel.First(x => x.ComponentId == "Manufacturing Overhead");
                    model.Corian = manufacturingModel.Corian / manHourModel.Corian;
                    model.Neolith = manufacturingModel.Neolith / manHourModel.Neolith;
                    model.Furniture = manufacturingModel.Furniture / manHourModel.Furniture;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                else if (emp == "Administrative Overhead 1 კაც/საათზე")
                {
                    OverheadsModel manHourModel = _OverheadsModel.First(x => x.ComponentId == "კაც/საათი");
                    OverheadsModel manufacturingModel = _OverheadsModel.First(x => x.ComponentId == "Administrative Overhead");
                    model.Corian = manufacturingModel.Corian / manHourModel.Corian;
                    model.Neolith = manufacturingModel.Neolith / manHourModel.Neolith;
                    model.Furniture = manufacturingModel.Furniture / manHourModel.Furniture;
                    model.Total = model.Corian + model.Neolith + model.Furniture;
                    model.ChangeDate = DateTime.Now;
                    model.ComponentId = emp;
                    model.ComponentName = emp;
                    _OverheadsModel.Add(model);
                }
                rec.MoveNext();
            }

            foreach (OverheadsModel overheadsModel in _OverheadsModel)
            {
                overheadsModel.AddOrUpdate();
            }
        }

    }
}
