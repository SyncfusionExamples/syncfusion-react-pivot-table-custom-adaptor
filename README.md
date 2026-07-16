<div align="center">
  <h1>Syncfusion® React Pivot Table – Custom Adaptor Quick Start</h1>

  <p>
    A production-ready quick start that connects the <strong>Syncfusion® React Pivot Table</strong> to an <strong>ASP.NET Core OData</strong> backend using a <strong>Custom Adaptor</strong> built on top of <code>ODataV4Adaptor</code> — enabling remote data binding with custom request/response processing and full CRUD operations.
  </p>

  <p>
    <a href="https://react.dev/"><img src="https://img.shields.io/badge/React-19-61DAFB?style=for-the-badge&logo=react&logoColor=white" alt="React"></a>
    <a href="https://dotnet.microsoft.com/apps/aspnet"><img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET"></a>
    <a href="https://www.syncfusion.com/react-components/react-pivot-table"><img src="https://img.shields.io/badge/Syncfusion-EJ2-FF9C00?style=for-the-badge&logo=syncfusion&logoColor=white" alt="Syncfusion"></a>
    <a href="https://github.com/SyncfusionExamples/custom-adaptor-with-pivot-table/blob/master/LICENSE"><img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" alt="License"></a>
  </p>
</div>

---

## 📑 Table of Contents

- [🚀 Quick Overview](#-quick-overview)
- [✨ Key Features](#-key-features)
- [🛠️ Prerequisites](#-prerequisites)
- [📂 Project Structure](#-project-structure)
- [🧠 Understanding the Custom Adaptor](#-understanding-the-custom-adaptor)
- [⚙️ Installation & Setup](#-installation--setup)
  - [1. Clone the Repository](#1-clone-the-repository)
  - [2. Backend – ASP.NET Core OData Service](#2-backend--aspnet-core-odata-service)
  - [3. Frontend – React Pivot Table](#3-frontend--react-pivot-table)
- [▶️ Running the Application](#-running-the-application)
- [🧪 Testing CRUD Operations](#-testing-crud-operations)
- [🔧 Troubleshooting](#-troubleshooting)
- [📖 API Reference](#-api-reference)
- [🤝 Contributing](#-contributing)
- [📜 License & Support](#-license--support)
- [📚 Related Resources](#-related-resources)

---

## 🚀 Quick Overview

This project demonstrates how to bind the **Syncfusion® React Pivot Table** to a remote **ASP.NET Core OData** backend using a **Custom Adaptor** that extends [`ODataV4Adaptor`](https://ej2.syncfusion.com/react/documentation/data/adaptors/odatav4-adaptor). A Custom Adaptor is not a standalone adaptor — it is a thin extension class that intercepts and customizes the data flow at three well-defined points (`processQuery`, `beforeSend`, `processResponse`) while reusing every other feature of the underlying base adaptor.

In this sample the `CustomAdaptor` class:

1. **Rewrites the API URL** to point at the ASP.NET Core OData endpoint (`/odata/Orders`).
2. **Appends a custom query parameter** (`Syncfusion Grid=true`) that the backend can read to identify requests originating from the Pivot Table.
3. **Injects an `Authorization` header** on every outgoing request.
4. **Augments each record** with a client-side `SNo` (serial number) field after the response is received.

| Component          | Technology                                       | Purpose                                                                |
| ------------------ | ------------------------------------------------ | ---------------------------------------------------------------------- |
| 🎨 Frontend        | React 19 + Syncfusion® EJ2                       | Render the interactive Pivot Table UI                                  |
| ⚙️ Backend         | ASP.NET Core 10.0 + OData (NewtonsoftJson)       | Serve OData-formatted data, perform CRUD via OData conventions         |
| 🔌 Adaptor         | `CustomAdaptor extends ODataV4Adaptor`           | Bridge between Pivot Table and OData service with custom hooks         |
| 📊 Sample Data     | In-memory `OrdersDetails` list (seeded)          | Two seeded order records used for the Pivot Table                      |

> 💡 A Custom Adaptor is the right choice when the built-in adaptors are *almost* sufficient but you need to tweak a few specific behaviors — for example adding auth headers, injecting custom query parameters, transforming responses, or attaching computed fields — without rewriting an entire adaptor.

---

## ✨ Key Features

- 📊 **Remote Data Binding** – Connects the Pivot Table to an ASP.NET Core OData endpoint over HTTP.
- 🧩 **Custom Adaptor Pattern** – Extends `ODataV4Adaptor` and overrides `processQuery`, `beforeSend`, and `processResponse`.
- 🔄 **Full CRUD Support** – Insert, update (PATCH), and delete records directly from the Pivot Table drill-through grid.
- 🗂️ **OData Query Support** – `$filter`, `$orderby`, `$top`, `$skip`, `$count` are produced by the base adaptor and honored by the backend via `[EnableQuery]`.
- 🛡️ **Property Casing Preservation** – Uses `DefaultContractResolver` to keep PascalCase fields like `OrderID`, `ShipCountry`.
- 🌐 **CORS-Enabled** – Preconfigured to allow cross-origin requests from the React dev server.
- ⚡ **Drill-Through Editing** – Double-click a pivot cell to add, edit, or delete underlying records in a pop-up grid.
- 🧮 **Client-Side Computed Fields** – `SNo` is added to every record on the client via `processResponse`.
- 🔐 **Authentication Header Injection** – Every request carries an `Authorization` header set inside `beforeSend`.
- 📦 **Ready-to-Run** – Clone, build, and start both projects — no database setup required (in-memory sample data).

---

## 🛠️ Prerequisites

Make sure the following software and packages are installed on your machine before running the project.

| Software / Package                          | Version       | Purpose                                                              |
| ------------------------------------------- | ------------- | -------------------------------------------------------------------- |
| 🟢 Node.js                                  | 18.x or later | Runtime for the React development server                             |
| ⚛️ React                                    | 19.x          | Build the Pivot Table client                                         |
| 🟣 .NET SDK                                 | 10.0 or later | Build and run the ASP.NET Core OData backend                         |
| 🧑‍💻 Visual Studio / VS Code                | Latest        | Configure and run the backend service                                |
| 📦 `@syncfusion/ej2-react-pivotview`        | 33.1.45+      | React Pivot Table component                                          |
| 📦 `@syncfusion/ej2-data`                   | 33.1.45+      | `DataManager`, `ODataV4Adaptor`, and `Query`                         |
| 📦 `@syncfusion/ej2-base`                   | 33.1.45+      | `setValue` helper used inside `processResponse`                      |
| 📦 `Microsoft.AspNetCore.OData`             | 9.4.1+        | OData endpoint support for ASP.NET Core                              |
| 📦 `Microsoft.AspNetCore.Mvc.NewtonsoftJson` | 10.0.9+      | Preserve original property casing during JSON serialization          |

---

## 📂 Project Structure

```
custom-adaptor-with-pivot-table/
├── 📁 Client/                              # React frontend (Pivot Table)
│   ├── 📁 public/
│   │   ├── index.html
│   │   ├── manifest.json
│   │   └── robots.txt
│   ├── 📁 src/
│   │   ├── App.css                         # Component styles
│   │   ├── App.js                          # Pivot Table + CustomAdaptor class
│   │   ├── App.test.js
│   │   ├── datasource.js                   # Local fallback sample data
│   │   ├── index.css
│   │   ├── index.js                        # React entry point
│   │   ├── reportWebVitals.js
│   │   └── setupTests.js
│   └── package.json                        # React dependencies & scripts
│
├── 📁 CustomAdaptor/                       # ASP.NET Core OData backend
│   ├── 📁 Controllers/
│   │   └── PivotController.cs              # OData endpoints: GET, POST, PATCH, DELETE
│   ├── 📁 Models/
│   │   └── OrdersDetails.cs                # Order data model + seeded sample data
│   ├── 📁 Properties/
│   │   └── launchSettings.json             # Default port: 5049 (HTTP)
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── CustomAdaptor.csproj                # Project file (net10.0)
│   ├── CustomAdaptor.http                  # Endpoint testing file
│   └── Program.cs                          # OData + Newtonsoft.Json + CORS configuration
│
├── 📄 README.md                            # You are here
```

---

## 🧠 Understanding the Custom Adaptor

A **Custom Adaptor** is a class that extends one of the built-in Syncfusion® adaptors (`UrlAdaptor`, `ODataV4Adaptor`, `WebApiAdaptor`, `GraphQLAdaptor`) and overrides a small set of lifecycle methods. The base adaptor continues to handle the heavy lifting (building the request, parsing the response, applying paging, etc.), while your subclass adds the *delta* your application needs.

This sample extends `ODataV4Adaptor` and overrides three methods:

| Method              | When it runs                                       | What it does in this sample                                                                       |
| ------------------- | -------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| `processQuery`      | Before the HTTP request is built                   | Rewrites `requestContext.dataSource.url` to `http://localhost:5049/odata/Orders` and calls `query.addParams('Syncfusion Grid', 'true')`. |
| `beforeSend`        | Immediately before the request is sent to the server | Sets the `Authorization` request header (sample value: `true`).                                  |
| `processResponse`   | After the server response is received              | Iterates `original.result` and calls `setValue('SNo', ++i, item)` to attach a serial number.      |

```jsx
// filepath: Client/src/App.js
import * as React from 'react';
import { PivotViewComponent } from '@syncfusion/ej2-react-pivotview';
import { DataManager, ODataV4Adaptor } from '@syncfusion/ej2-data';
import { setValue } from '@syncfusion/ej2-base';
import './App.css';

class CustomAdaptor extends ODataV4Adaptor {
    processQuery(dm, query) {
        dm.dataSource.url = 'http://localhost:5049/odata/Orders'; // Here xxxx represents the port number.
        query.addParams('Syncfusion Grid', 'true');               // Add the additional parameter.
        return super.processQuery.apply(this, arguments);
    }

    beforeSend(dm, request, settings) {
        request.headers.set('Authorization', `true`);
        super.beforeSend(dm, request, settings);
    }

    processResponse() {
        let i = 0;
        const original = super.processResponse.apply(this, arguments);
        // Adding serial number.
        if (original.result) {
            original.result.forEach((item) => setValue('SNo', ++i, item));
        }
        return original;
    }
}
```

> 📝 Because the base adaptor is `ODataV4Adaptor`, the request URL produced by Syncfusion® uses OData v4 query syntax (`$filter`, `$orderby`, `$top`, `$skip`, `$count`) and the response is expected in the OData v4 shape: `{ "@odata.count": N, "value": [...] }`. The backend is configured to honor exactly that contract.

---

## ⚙️ Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/SyncfusionExamples/custom-adaptor-with-pivot-table.git
cd custom-adaptor-with-pivot-table
```

### 2. Backend – ASP.NET Core OData Service

The backend project lives in the `CustomAdaptor/` folder.

#### 2.1 Restore dependencies

```bash
cd CustomAdaptor
dotnet restore
```

#### 2.2 Verify the required NuGet packages

`CustomAdaptor.csproj` references:

- `Microsoft.AspNetCore.OData` — OData routing and `[EnableQuery]` support.
- `Microsoft.AspNetCore.Mvc.NewtonsoftJson` — serializes properties using their original PascalCase names (`OrderID`, `ShipCountry`, etc.) instead of the default camelCase.
- `Microsoft.AspNetCore.OpenApi` — OpenAPI metadata for development exploration.

```bash
dotnet add package Microsoft.AspNetCore.OData
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.AspNetCore.OpenApi
```

#### 2.3 Inspect the configuration

`Program.cs` registers:

- ✅ **OData** with `ODataConventionModelBuilder` exposing the `OrdersDetails` model under the entity set name `Pivot` and mounted at the `odata` route prefix (so the final URL is `/odata/Orders`).
- ✅ **NewtonsoftJson** with `DefaultContractResolver` to preserve property casing.
- ✅ **CORS** with `AllowAnyOrigin` for development (restrict this in production).
- ✅ **OpenAPI** mapped at `/openapi` in the Development environment.

```csharp
// filepath: CustomAdaptor/Program.cs
using CustomAdaptor.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Newtonsoft.Json.Serialization;

// Create an ODataConventionModelBuilder to build the OData model.
var modelBuilder = new ODataConventionModelBuilder();

// Register the orders entity set with the OData model builder.
modelBuilder.EntitySet<OrdersDetails>("Pivot");

var builder = WebApplication.CreateBuilder(args);

// Add controllers with OData support to the service collection.
builder.Services.AddControllers().AddOData(
    options => options
        .Count()
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyOrigin()      // Allow requests from any origin.
          .AllowAnyMethod()       // Allow GET, POST, PUT, DELETE, etc.
          .AllowAnyHeader();      // Allow any request headers.
  });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();

app.Run();
```

> 🔒 **Production CORS:** Replace `AllowAnyOrigin()` with `policy.WithOrigins("https://yourdomain.com")`.
> 📌 **Default port:** The HTTP launch profile in `Properties/launchSettings.json` listens on `http://localhost:5049`. The HTTPS profile also exposes `http://localhost:5049` alongside `https://localhost:7018`. Update the URL inside `Client/src/App.js` if you change this.

#### 2.4 The data model

`Models/OrdersDetails.cs` defines the entity exposed through OData. It is seeded with two records (`OrderID 10001` and `10002`) the first time `GetAllRecords()` is called.

```csharp
// filepath: CustomAdaptor/Models/OrdersDetails.cs
using System.ComponentModel.DataAnnotations;

namespace CustomAdaptor.Models
{
    public class OrdersDetails
    {
        public static List<OrdersDetails> order = new List<OrdersDetails>();

        public OrdersDetails() { }

        public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, string ShipCountry)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.ShipCountry = ShipCountry;
        }

        public static List<OrdersDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 2; i++)
                {
                    order.Add(new OrdersDetails(code + 1, "ALFKI", i + 0, "Denmark"));
                    order.Add(new OrdersDetails(code + 2, "ANATR", i + 2, "Brazil"));
                    code += 5;
                }
            }
            return order;
        }

        [Key]
        public int? OrderID { get; set; }
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public string? ShipCountry { get; set; }
    }
}
```

> ℹ️ Several constructors in `GetAllRecords()` are commented out in the source. The endpoint currently serves only the two uncommented records. Uncomment the remaining lines (or extend the seed loop) to load more data.

### 3. Frontend – React Pivot Table

The React client lives in the `Client/` folder.

#### 3.1 Install npm dependencies

```bash
cd ../Client
npm install
```

#### 3.2 Verify the API URL

Open `src/App.js` and confirm the `url` set inside `processQuery` matches your backend port. The sample uses the HTTP profile (`http://localhost:5049`).

```jsx
// filepath: Client/src/App.js
function App() {
    // Configure DataManager with the CustomAdaptor.
    const data = new DataManager({
        url: 'http://localhost:5049/odata/Orders',
        adaptor: new CustomAdaptor(),
    });

    const editSettings = {
        allowEditing: true,    // Enables the Edit button and allows users to modify existing records
        allowAdding: true,     // Enables the Add button and allows users to create new records
        allowDeleting: true,   // Enables the Delete button and allows users to remove records
        mode: 'Normal'         // Uses Normal mode (popup dialog) for editing; other options: 'Dialog', 'Batch'
    };

    const dataSourceSettings = {
        dataSource: data,
        expandAll: false,
        rows: [{ name: 'ShipCountry' }],
        columns: [{ name: 'OrderID' }],
        values: [{ name: 'EmployeeID' }],
        formatSettings: [{ name: 'Freight', format: 'N0' }],
    };

    let pivotObj;

    // Configure beginDrillThrough event to set the primary key for CRUD operations
    function beginDrillThrough(args) {
        // Iterate through all columns in the drill-through grid
        for (var i = 0; i < args.gridObj.columns.length; i++) {
            // Check if the current column is the primary key column
            if (args.gridObj.columns[i].field === "OrderID") {
                // Mark this column as the primary key
                // This tells DataManager to use this column's value to uniquely identify records
                args.gridObj.columns[i].isPrimaryKey = true;
            } else {
                // Make all other columns visible so users can view and edit them
                args.gridObj.columns[i].visible = true;
                // Configure the edit type for date fields to use a date picker for editing
                if (args.gridObj.columns[i].field === 'OrderDate' || args.gridObj.columns[i].field === 'ShippedDate') {
                    args.gridObj.columns[i].editType = 'datetimepickeredit';
                }
            }
        }
    }

    return (
        <div className='control-section' style={{ margin: 100 }}>
            <PivotViewComponent
                ref={d => pivotObj = d}
                id='PivotView'
                height={350}
                width={700}
                editSettings={editSettings}
                beginDrillThrough={beginDrillThrough}
                dataSourceSettings={dataSourceSettings}>
            </PivotViewComponent>
        </div>
    );
}

export default App;
```

> 🔑 `OrderID` is marked as the primary key inside the `beginDrillThrough` event so update and delete operations know which record to target.

---

## ▶️ Running the Application

You need **two terminals** — one for the backend OData service and one for the React client.

### ▶️ Start the Backend (Terminal 1)

```bash
cd CustomAdaptor
dotnet run
```

The service will start on `http://localhost:5049` (HTTP profile) by default. The HTTPS profile also binds `https://localhost:7018` if you select that profile.

**Verify it works:**

- 🌐 Open `http://localhost:5049/odata/Orders` in your browser.
- ✅ You should see an OData v4 response: `{ "@odata.context": "...", "value": [ ... ] }`.
- 🔢 Visit `http://localhost:5049/odata/Orders?$count=true` to confirm that the inline `@odata.count` is returned (required for paging).
- 📘 In Development you can also browse `http://localhost:5049/openapi` for the generated OpenAPI document.

> 📝 Note the port number in the terminal output and update `dm.dataSource.url` inside `processQuery` in [Client/src/App.js](Client/src/App.js) if it is different from `5049`.

### ▶️ Start the Frontend (Terminal 2)

```bash
cd Client
npm start
```

The React app will open at `http://localhost:3000` by default. 🎉

You should see the Pivot Table populated with aggregated **EmployeeID** values, grouped by **ShipCountry** (rows) and **OrderID** (columns). The serialized response also includes an `SNo` field on every record that you can verify in the **Network** tab.

### ✅ Verify in the Browser

1. Open the browser's **Developer Tools** (F12) → **Network** tab.
2. Reload the page.
3. You should see a GET request to `http://localhost:5049/odata/Orders` (with `$count=true`) that returns status `200` and a JSON response containing `@odata.count` and `value`.
4. Inspect the request headers and confirm `Authorization: true` is present.
5. Inspect any record in the response and confirm an `SNo` field was added by `processResponse`.
6. The Pivot Table renders the aggregated data automatically.

---

## 🧪 Testing CRUD Operations

The Pivot Table supports full CRUD through its built-in **drill-through editing** grid. Because the underlying adaptor is `ODataV4Adaptor`, the Syncfusion® DataManager issues the standard OData HTTP verbs: `GET`, `POST`, `PATCH`, and `DELETE`.

| Step | Action                                                                                                  | Expected Network Request                                                |
| ---- | ------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| 1️⃣  | **Double-click** any pivot cell to open the drill-through grid showing underlying source records.       | `GET http://localhost:5049/odata/Orders?$count=true`                     |
| ➕ 2️⃣ | Click **Add**, fill in the new row fields, then click **Update**.                                       | `POST http://localhost:5049/odata/Orders`                               |
| ✏️ 3️⃣ | Click **Edit** on an existing row, change a field, then click **Update**.                                | `PATCH http://localhost:5049/odata/Orders/{OrderID}`                    |
| 🗑️ 4️⃣ | Click **Delete** on a row to remove it.                                                                  | `DELETE http://localhost:5049/odata/Orders/{OrderID}`                   |
| 🔁 5️⃣ | The Pivot Table automatically refreshes to display the updated aggregated data from the backend.        | New `GET` to `/odata/Orders?$count=true`                                 |

> 🔑 The `OrderID` column is automatically marked as the primary key inside the `beginDrillThrough` event, so update and delete operations know which record to target.
> 🧪 You can also exercise the endpoints directly using the included [`CustomAdaptor.http`](CustomAdaptor/CustomAdaptor.http) file in JetBrains Rider / VS Code, or via `curl` / Postman.

---

## 🔧 Troubleshooting

| ❓ Issue                                | 🔍 Symptom                                                                                                       | ✅ Resolution                                                                                                                |
| --------------------------------------- | ---------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| 🚫 Empty Pivot Table                    | Pivot loads with no errors but no rows or values appear.                                                          | Ensure the OData response contains `value` (and `@odata.count` for paging) and that field names match `dataSourceSettings` (case-sensitive). |
| 404 Not Found                           | Network tab shows a 404 response when the Pivot Table loads.                                                     | Confirm the backend is running, the OData route is `odata/Pivot`, and the URL in `App.js` matches the API port.               |
| 💥 500 Internal Server Error            | The Pivot Table fails and the browser shows a server error.                                                      | Check the terminal/Visual Studio output for stack traces. Common causes: null reference, missing `[EnableQuery]`, or CORS issues. |
| 🌐 CORS Blocked                         | Console shows `Access to XMLHttpRequest ... has been blocked by CORS policy`.                                    | Verify CORS is configured in `Program.cs` and `app.UseCors()` is called **before** `app.MapControllers()`.                    |
| 💾 CRUD operations not saving           | The edit dialog closes but changes are not reflected in the data.                                                | Confirm the primary key is set in `beginDrillThrough` and that `POST`/`PATCH`/`DELETE` are mapped on `PivotController`.       |
| 🔤 Property casing mismatch             | Pivot appears empty or shows "field not found" even though the API returns data.                                 | Ensure `DefaultContractResolver` is registered in `Program.cs` so PascalCase fields like `OrderID` are preserved.             |
| 🧮 Serial numbers missing on records    | `SNo` does not appear in the rendered data.                                                                      | Verify `processResponse` runs `super.processResponse` first and uses `setValue('SNo', ++i, item)` on each item.               |
| 🛂 Auth header not visible              | The `Authorization` header is missing on the outgoing request.                                                   | Confirm `beforeSend` calls `request.headers.set('Authorization', ...)` and then `super.beforeSend`.                            |
| 🔁 Adaptor does nothing                 | Overrides have no effect.                                                                                        | Ensure the `DataManager` is constructed with `new CustomAdaptor()` (not `new ODataV4Adaptor()`) and that `super.<method>` is called exactly once per method. |
| 🔒 SSL/TLS certificate error            | Console shows `net::ERR_CERT_AUTHORITY_INVALID` or browser warns about an untrusted certificate.                 | Run `dotnet dev-certs https --clean` then `dotnet dev-certs https --trust` (Windows/macOS). On Linux, use HTTP for local testing. |

---

## 📖 API Reference

The backend exposes the following endpoints through `PivotController` (mounted under the OData route prefix `odata`, entity set `Pivot`):

| Method     | Route                        | Purpose                                                                 | Request Body                  | Response            |
| ---------- | ---------------------------- | ----------------------------------------------------------------------- | ----------------------------- | ------------------- |
| `GET`      | `/odata/Orders`               | Retrieve all order records (supports `$filter`, `$orderby`, `$top`, `$skip`, `$count`). | —                             | OData v4 payload (`{ "@odata.count": N, "value": [...] }`) |
| `POST`     | `/odata/Orders`               | Insert a new order.                                                     | `OrdersDetails` JSON          | `200 OK` with the inserted record |
| `PATCH`    | `/odata/Orders/{key}`         | Update `CustomerID`, `EmployeeID`, and `ShipCountry` of an existing order. | `OrdersDetails` JSON          | `200 OK` with the updated record |
| `DELETE`   | `/odata/Orders/{key}`         | Remove an order by primary key (`OrderID`).                             | —                             | `200 OK` with the deleted record |

The `OrdersDetails` model exposes the following fields:

| Field         | Type        | Description                                                       |
| ------------- | ----------- | ----------------------------------------------------------------- |
| `OrderID`     | `int?`      | Unique order identifier (primary key, `[Key]` annotated).         |
| `CustomerID`  | `string?`   | Identifier of the customer.                                       |
| `EmployeeID`  | `int?`      | Identifier of the handling employee.                              |
| `ShipCountry` | `string?`   | Destination country.                                              |

> ℹ️ The current controller updates only `CustomerID`, `EmployeeID`, and `ShipCountry` on PATCH. Extend `Patch` in [`PivotController.cs`](CustomAdaptor/Controllers/PivotController.cs) if you need to update other fields.

### Data flow summary

```
┌──────────────────────────┐         ┌──────────────────────────┐         ┌────────────────────────┐
│  React Pivot Table       │         │  CustomAdaptor           │         │  ASP.NET Core OData    │
│  (Syncfusion® EJ2)       │  HTTP   │  extends ODataV4Adaptor  │  HTTP   │  PivotController       │
│                          │ ──────► │  - processQuery          │ ──────► │  GET  /odata/Orders     │
│  drill-through CRUD →    │         │  - beforeSend            │         │  POST /odata/Orders     │
│  aggregates → render     │ ◄────── │  - processResponse       │ ◄────── │  PATCH/PUT/DELETE      │
└──────────────────────────┘   JSON  └──────────────────────────┘   JSON  └────────────────────────┘
```

1. The Pivot Table triggers a query through the `DataManager`.
2. `CustomAdaptor.processQuery` rewrites the URL to `/odata/Orders` and adds the `Syncfusion Grid=true` query parameter.
3. `CustomAdaptor.beforeSend` injects the `Authorization` header.
4. The base `ODataV4Adaptor` serializes the request as OData v4 (`$filter`, `$top`, etc.) and calls the backend.
5. `PivotController` honors the query through `[EnableQuery]` and returns `{ "@odata.count": N, "value": [...] }`.
6. `CustomAdaptor.processResponse` walks `original.result` and adds an `SNo` field to each item before the Pivot Table renders it.
7. CRUD operations (POST / PATCH / DELETE) follow the same flow but use the verbs described in the [API Reference](#-api-reference) table.

---

## 🤝 Contributing

Contributions are welcome and appreciated! 💖

1. 🍴 **Fork** the repository.
2. 🌿 **Create** a feature branch: `git checkout -b feature/my-awesome-change`
3. 💾 **Commit** your changes: `git commit -m "Add my awesome change"`
4. 📤 **Push** to your branch: `git push origin feature/my-awesome-change`
5. 🔁 **Open** a Pull Request describing the change and its motivation.

### 📋 Contribution Guidelines

- Follow the existing code style in both the React and ASP.NET Core projects.
- Keep changes focused — one feature or fix per pull request.
- Update or add documentation (`README.md`, `custom-adaptor.md`) when behavior changes.
- Test your changes locally against both the backend and frontend before submitting.

---

## 📜 License & Support

### 📄 License

This project is released under the **MIT License**. You are free to use, modify, and distribute the code in personal and commercial projects. See the [LICENSE](LICENSE) file for full text.

### 🛟 Support

- 📘 **Documentation:** [Syncfusion® React Pivot Table Docs](https://ej2.syncfusion.com/react/documentation/pivotview/getting-started)
- 💬 **Community forum:** [Syncfusion® Community](https://www.syncfusion.com/forums)
- 🐛 **Bug reports & feature requests:** [GitHub Issues](https://github.com/SyncfusionExamples/custom-adaptor-with-pivot-table/issues)
- 📧 **Direct support:** [Syncfusion® Support Portal](https://www.syncfusion.com/support) (for licensed users)
- 📖 **Custom Adaptor Guide:** [Custom Adaptor Documentation](https://ej2.syncfusion.com/react/documentation/data/adaptors/custom-adaptor)

> ⭐ If this project helped you, please consider giving it a **star** on GitHub — it helps others discover it!

---

## 📚 Related Resources

- 🔗 [WebApiAdaptor with Pivot Table](https://github.com/SyncfusionExamples/webapi-adaptor-with-pivot-table) — Companion sample using `WebApiAdaptor` instead of a Custom Adaptor.
- 🔗 [UrlAdaptor with Pivot Table](https://github.com/SyncfusionExamples/url-adaptor-with-pivot-table) — Companion sample using `UrlAdaptor`.
- 📘 [PivotTable Data Binding](https://ej2.syncfusion.com/react/documentation/pivotview/data-binding)
- 📘 [DataManager Getting Started](https://ej2.syncfusion.com/react/documentation/data/getting-started)
- 📘 [ODataV4Adaptor Reference](https://ej2.syncfusion.com/react/documentation/data/adaptors/odatav4-adaptor)
- 📘 [Custom Adaptor Reference](https://ej2.syncfusion.com/react/documentation/data/adaptors/custom-adaptor)
- 📘 [PivotTable Editing](https://ej2.syncfusion.com/react/documentation/pivotview/editing)
- 📘 [PivotTable Drill-Through](https://ej2.syncfusion.com/react/documentation/pivotview/drill-through)

---

<div align="center">
  <sub>Built with ❤️ using <a href="https://react.dev/">React</a> and <a href="https://dotnet.microsoft.com/">.NET</a> by the <a href="https://www.syncfusion.com/">Syncfusion®</a> team.</sub>
</div>
