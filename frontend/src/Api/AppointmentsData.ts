import { mainBackendAddress } from "./GeneralData";

export interface AppointmentData {
  id: string;
  patientCardId: string;
  patientFirstName: string;
  patientLastName: string;
  doctorId: string;
  doctorUserId: string;
  doctorFirstName: string;
  doctorLastName: string;
  description: string;
  illnessId: string;
  illnessName: string;
  startDate: Date;
  startTime: string;
  endDate: Date;
  completed: boolean;
}

export interface EditAppointmentForm {
  description: string;
  illnessId?: string | null;
  endDate: Date;
  completed: boolean;
}

export interface CreateAppointmentForm {
  patientCardId: string;
  doctorId: string;
  description: string;
  startDate: string;
  startTime: string;
}

export interface ResponseAppointents {
  data: AppointmentData[];
  responseStatus: number;
}

export interface ResponseAppointment {
  data: AppointmentData | null;
  responseStatus: number;
}

export interface AppointmentParameters {
  orderBy?: string;
  date?: string;
}

const getHeaders = (token?: string): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", mainBackendAddress);
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", mainBackendAddress);
  headers.append("Authorization", "bearer " + token);

  return headers;
};

export const getAllAppointmentsForDoctor = async (
  userId?: string,
  token?: string,
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/all/forDoctor/" + userId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401)
    return { data: appointments, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: appointments, responseStatus: response.status };

  appointments = await response.json();

  return { data: appointments, responseStatus: 200 };
};

export const getAppointmentsForDoctor = async (
  userId?: string,
  token?: string,
  parameters?: AppointmentParameters,
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  var query =
    parameters === undefined || parameters.date === undefined
      ? ""
      : "?" +
        JSON.stringify(parameters, function (key, value) {
          if (
            value === undefined ||
            value === "" ||
            value.toString().includes("&")
          )
            return undefined;
          return value;
        })
          .replaceAll("{", "")
          .replaceAll("}", "")
          .replaceAll(":", "=")
          .replaceAll('"', "")
          .replaceAll(",", "&");

  const response = await fetch(
    mainBackendAddress + "/api/appointments/forDoctor/" + userId + query,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401)
    return { data: appointments, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: appointments, responseStatus: response.status };

  appointments = await response.json();

  return { data: appointments, responseStatus: 200 };
};

export const getAppointmentDatesForDoctor = async (
  userId?: string,
  token?: string,
): Promise<Date[]> => {
  let appointmentDates: Date[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/dates/forDoctor/" + userId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return appointmentDates;
  else if (response.status !== 200) return appointmentDates;

  appointmentDates = await response.json();

  return appointmentDates;
};

export const getAllAppointmentsForPatientCard = async (
  patientCardId?: string,
  token?: string,
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/all/forPatient/" + patientCardId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401)
    return { data: appointments, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: appointments, responseStatus: response.status };

  appointments = await response.json();

  return { data: appointments, responseStatus: 200 };
};

export const getAppointmentsForPatientCard = async (
  patientCardId?: string,
  token?: string,
  parameters?: AppointmentParameters,
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  var query =
    parameters === undefined || parameters.date === undefined
      ? ""
      : "?" +
        JSON.stringify(parameters, function (key, value) {
          if (
            value === undefined ||
            value === "" ||
            value.toString().includes("&")
          )
            return undefined;
          return value;
        })
          .replaceAll("{", "")
          .replaceAll("}", "")
          .replaceAll(":", "=")
          .replaceAll('"', "")
          .replaceAll(",", "&");

  const response = await fetch(
    mainBackendAddress +
      "/api/appointments/forPatient/" +
      patientCardId +
      query,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401)
    return { data: appointments, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: appointments, responseStatus: response.status };

  appointments = await response.json();

  return { data: appointments, responseStatus: 200 };
};

export const getAppointmentDatesForPatientCard = async (
  patientCardId?: string,
  token?: string,
): Promise<Date[]> => {
  let appointmentDates: Date[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/dates/forPatient/" + patientCardId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return appointmentDates;
  else if (response.status !== 200) return appointmentDates;

  appointmentDates = await response.json();

  return appointmentDates;
};

export const getAppointment = async (
  appointmentId?: string,
  token?: string,
): Promise<ResponseAppointment> => {
  let appointment = null;

  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/" + appointmentId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401)
    return { data: appointment, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: appointment, responseStatus: response.status };

  appointment = await response.json();

  return { data: appointment, responseStatus: 200 };
};

export const getAppointmentAmount = async (token?: string): Promise<number> => {
  let headers = getHeaders(token);

  let response = await fetch(mainBackendAddress + "/api/appointments/amount", {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status !== 200) return 0;

  var result = await response.json();

  return result;
};

export const getCompletedAppointmentAmount = async (
  token?: string,
): Promise<number> => {
  let headers = getHeaders(token);

  let response = await fetch(
    mainBackendAddress + "/api/appointments/completedAmount",
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status !== 200) return 0;

  var result = await response.json();

  return result;
};

export const ifUserCanEditAppointment = async (
  appointmentId?: string,
  userId?: string,
  token?: string,
): Promise<boolean> => {
  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress +
      "/api/appointments/" +
      appointmentId +
      "/canUserChange/" +
      userId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return false;
  else if (response.status !== 200) return false;

  let result = await response.json();

  return result;
};

export const editAppointment = async (
  appointment: EditAppointmentForm,
  appointmentId?: string,
  token?: string,
): Promise<string[]> => {
  if (appointment.completed && appointment.endDate == null)
    appointment.endDate = new Date();
  if (appointment.illnessId === "") appointment.illnessId = null;

  let headers = getHeaders(token);

  let response = await fetch(
    mainBackendAddress + "/api/appointments/" + appointmentId,
    {
      mode: "cors",
      method: "PUT",
      body: JSON.stringify(appointment),
      headers: headers,
    },
  );

  if (response.status === 401) return ["Unauthorized"];
  if (response.status === 200) return [];
  let result = await response.json();

  return result.errors !== null ? result.errors : [];
};

export const createAppointment = async (
  appointment: CreateAppointmentForm,
  token?: string,
): Promise<string[]> => {
  let headers = getHeaders(token);

  const response = await fetch(mainBackendAddress + "/api/appointments", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(appointment),
    headers: headers,
  });

  if (response.status === 401) return ["Unauthorized"];

  let result = await response.json();

  if (result.errors) return result.errors;

  return ["Its OK: " + result];
};

export const deleteAppointment = async (
  appointmentId?: string,
  token?: string,
): Promise<boolean> => {
  let headers = getHeaders(token);

  const response = await fetch(
    mainBackendAddress + "/api/appointments/" + appointmentId,
    {
      mode: "cors",
      method: "DELETE",
      headers: headers,
    },
  );

  if (response.status === 401) return false;
  else if (response.status !== 204) return false;

  return true;
};
