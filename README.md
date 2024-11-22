**Meter Reading Importer**

Features
<ul>
  <li>Upload and process meter readings via file uploads.</li>
  <li>Validate account existence before processing readings.</li>
  <li>Prevent duplicate and outdated readings from being saved.</li>
  <li>Return statistics on successful and failed readings.</li>
</ul>

**Technology Stack**
<ul>
  <li>ASP.NET Core 8.0</li>
  <li>Database: Postgres</li>
  <li>Dependency Injection: Built-in DI container.</li>
  <li>Logging: Built-in logging framework.</li>
  <li>Unit Tests: XUnit.</li>
</ul>

**How It Works**
1. File Upload: Users upload a file containing meter readings.
2. Validation:
  - Checks if accounts in the readings exist.
  - Filters out duplicate and outdated readings.
3. Storage: Valid readings are saved in the database and statistics are generated and sent to clients.
