# Disaster Prediction and Alert System API (Mini Project)

## 📌 Project Overview
This project is a RESTful API built for a government agency to predict potential disaster risks, such as floods, earthquakes, and wildfires, for specified regions. The system acts as a backend engine that gathers real-time environmental data through external API integrations and utilizes a scoring algorithm to assess risk levels and send alerts to affected communities.

## Tech Stack
* **Framework:** .NET Core / ASP.NET Core Web API (C#)
* **Database:** SQL Server / Entity Framework Core (EF Core)
* **Caching:** Redis Distributed Cache
* **Cloud & Deployment:** Microsoft Azure (App Service)
* **External APIs:** OpenWeather API, USGS Earthquake API

## 🚀 Key Features & Architecture
* **External Data Integration:** Fetches real-time environmental data by integrating with external sources like OpenWeather and USGS (for seismic activity, rainfall, and temperature).
* **Dynamic Risk Calculation:** Calculates risk scores for each disaster type based on the fetched environmental data and compares them against user-configured thresholds.
* **Redis Caching:** Implements data caching with a 15-minute expiration to store fetched data and risk scores, significantly reducing redundant external API calls and improving system performance.
* **Robust Error Handling:** Designed to gracefully manage failure scenarios, including failed external API calls, missing external data, and regions with no available data.
* **Messaging & Alerting:** Includes a mock alert-sending service designed to notify people in high-risk regions via messaging APIs (structured and ready for Twilio/SendGrid integration).
* **System Logging:** Tracks all API usage, alert activities, and external API calls for auditing, monitoring, and debugging purposes.
* **Cloud Ready:** The scalable solution is fully deployed and hosted on Azure.

## API Endpoints

### 1. Region Management
* `POST /api/regions`: Allows users to add regions with specific location coordinates and the types of disasters they want to monitor.
* `POST /api/alert-settings`: Allows users to configure alert settings for each region, including custom thresholds for disaster risk scores.

### 2. Risk Assessment
* `GET /api/disaster-risks`: Triggers the core disaster risk assessment. This endpoint fetches real-time environmental data, calculates risk scores, and returns risk levels (Low, Medium, High) for each region, indicating if any alerts should be sent.

### 3. Alert System
* `POST /api/alerts/send`: Sends an alert for regions identified as high-risk and stores the alert record in the database.
* `GET /api/alerts`: Returns a list of recent alerts for each region that are stored in the database.

---

## 📊 Data Structures & JSON Examples

### Input Data
**1. Regions (`POST /api/regions`)**
* **Region ID**: Unique identifier for each region.
* **Location Coordinates**: Latitude and longitude of the region.
* **Disaster Types**: List of disaster types to monitor (e.g., flood, wildfire, earthquake).

```json
[
  {
    "RegionID": "R1",
    "LocationCoordinates": { "latitude": 34.0522, "longitude": -118.2437 },
    "DisasterTypes": ["flood", "earthquake"]
  },
  {
    "RegionID": "R2",
    "LocationCoordinates": { "latitude": 36.7783, "longitude": -119.4179 },
    "DisasterTypes": ["wildfire"]
  }
]
```

**2. Alert Settings (`POST /api/alert-settings`)**
* **Region ID**: Identifier for the region.
* **Disaster Type**: Type of disaster (must match one monitored by the region).
* **Threshold Score**: Risk score threshold that triggers an alert for this disaster type.

```json
[
  {
    "RegionID": "R1",
    "DisasterType": "flood",
    "ThresholdScore": 75
  },
  {
    "RegionID": "R2",
    "DisasterType": "wildfire",
    "ThresholdScore": 80
  }
]
```

### Output Data
**Disaster Risk Report (`GET /api/disaster-risks`)**
Returns a list for each region containing Region ID, Disaster Type, Risk Score, Risk Level (Low, Medium, or High), and Alert Triggered (True/False).

```json
[
  {
    "RegionID": "R1",
    "DisasterType": "flood",
    "RiskScore": 82,
    "RiskLevel": "High",
    "AlertTriggered": true
  },
  {
    "RegionID": "R2",
    "DisasterType": "wildfire",
    "RiskScore": 65,
    "RiskLevel": "Medium",
    "AlertTriggered": false
  }
]
```
