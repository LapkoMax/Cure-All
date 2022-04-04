import { DoctorData } from "../../Api/DoctorsData";
import {
  gettingDoctorAction,
  gettingDoctorsAction,
  gotDoctorAction,
  gotDoctorsAction,
} from "../ActionCreators/DoctorActionCreators";
import {
  GETTINGDOCTOR,
  GETTINGDOCTORS,
  GOTDOCTOR,
  GOTDOCTORS,
} from "../Actions/DoctorActions";

type DoctorsActions =
  | ReturnType<typeof gettingDoctorsAction>
  | ReturnType<typeof gotDoctorsAction>
  | ReturnType<typeof gettingDoctorAction>
  | ReturnType<typeof gotDoctorAction>;

export interface DoctorsState {
  readonly loading: boolean;
  readonly doctors: DoctorData[];
  readonly doctor?: DoctorData | null;
}

const initialDoctorsState: DoctorsState = {
  loading: false,
  doctors: [],
  doctor: undefined,
};

export const doctorsReducer = (
  state = initialDoctorsState,
  action: DoctorsActions,
) => {
  switch (action.type) {
    case GETTINGDOCTORS: {
      return {
        ...state,
        doctors: [],
        loading: true,
      };
    }
    case GOTDOCTORS: {
      return {
        ...state,
        loading: false,
        doctors: action.doctors,
      };
    }
    case GETTINGDOCTOR: {
      return {
        ...state,
        loading: true,
        doctor: undefined,
      };
    }
    case GOTDOCTOR: {
      return {
        ...state,
        loading: false,
        doctor: action.doctor,
      };
    }
  }
  return state;
};
