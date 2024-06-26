﻿using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetColumnDistinctValues;

public class GetUserColumnDistinctValuesQuery : ColumnDistinctValueCursoredQueryModel, IQuery<CursoredResponse<object>>;
