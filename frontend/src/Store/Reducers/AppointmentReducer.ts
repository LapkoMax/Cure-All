import { AppointmentData } from "../../Api/AppointmentsData";
import {
  gettingAppointmentAction,
  gettingAppointmentsAction,
  gotAppointmentAction,
  gotAppointmentsAction,
} from "../ActionCreators/AppointmentActionCreators";
import {
  GETTINGAPPOINTMENT,
  GETTINGAPPOINTMENTS,
  GOTAPPOINTMENT,
  GOTAPPOINTMENTS,
} from "../Actions/AppointmentActions";

type AppointmentsAction =
  | ReturnType<typeof gettingAppointmentsAction>
  | ReturnType<typeof gotAppointmentsAction>
  | ReturnType<typeof gettingAppointmentAction>
  | ReturnType<typeof gotAppointmentAction>;

export interface AppointmentsState {
  readonly loading: boolean;
  readonly appointments: AppointmentData[];
  readonly appointment?: AppointmentData | null;
}

const initialAppointmentsState: AppointmentsState = {
  loading: false,
  appointments: [],
  appointment: null,
};

export const appointmentsReducer = (
  state = initialAppointmentsState,
  action: AppointmentsAction,
) => {
  switch (action.type) {
    case GETTINGAPPOINTMENTS: {
      return {
        ...state,
        loading: true,
        appointments: [],
      };
    }
    case GOTAPPOINTMENTS: {
      return {
        ...state,
        loading: false,
        appointments: action.appointments,
      };
    }
    case GETTINGAPPOINTMENT: {
      return {
        ...state,
        appointment: null,
        loading: true,
      };
    }
    case GOTAPPOINTMENT: {
      return {
        ...state,
        appointment: action.appointment,
        loading: false,
      };
    }
  }
  return state;
};
