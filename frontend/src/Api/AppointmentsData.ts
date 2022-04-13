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
  endDate: Date;
  completed: boolean;
}

export interface EditAppointmentForm {
  description: string;
  illnessId?: string | null;
  endDate: Date;
  completed: boolean;
}

export interface ResponseAppointents {
  data: AppointmentData[];
  responseStatus: number;
}

export interface ResponseAppointment {
  data: AppointmentData | null;
  responseStatus: number;
}

const getHeaders = (token?: string): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", "http://localhost:5000");
  headers.append("Authorization", "bearer " + token);

  return headers;
};

export const getAppointmentsForDoctor = async (
  userId?: string,
  token?: string,
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/appointments/forDoctor/" + userId,
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
): Promise<ResponseAppointents> => {
  let appointments: AppointmentData[] = [];

  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/appointments/forPatient/" + patientCardId,
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

export const getAppointment = async (
  appointmentId?: string,
  token?: string,
): Promise<ResponseAppointment> => {
  let appointment = null;

  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/appointments/" + appointmentId,
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

export const ifUserCanEditAppointment = async (
  appointmentId?: string,
  userId?: string,
  token?: string,
): Promise<boolean> => {
  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/appointments/" +
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
    "http://localhost:5000/api/appointments/" + appointmentId,
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

export const deleteAppointment = async (
  appointmentId?: string,
  token?: string,
): Promise<boolean> => {
  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/appointments/" + appointmentId,
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
