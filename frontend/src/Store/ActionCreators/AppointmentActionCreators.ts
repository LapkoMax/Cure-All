import { AppointmentData } from "../../Api/AppointmentsData";
import {
  GETTINGAPPOINTMENT,
  GETTINGAPPOINTMENTS,
  GOTAPPOINTMENT,
  GOTAPPOINTMENTS,
} from "../Actions/AppointmentActions";

export const gettingAppointmentsAction = () =>
  ({
    type: GETTINGAPPOINTMENTS,
  } as const);

export const gotAppointmentsAction = (appointments: AppointmentData[]) =>
  ({
    type: GOTAPPOINTMENTS,
    appointments: appointments,
  } as const);

export const gettingAppointmentAction = () =>
  ({ type: GETTINGAPPOINTMENT } as const);

export const gotAppointmentAction = (appointment: AppointmentData | null) =>
  ({ type: GOTAPPOINTMENT, appointment: appointment } as const);
