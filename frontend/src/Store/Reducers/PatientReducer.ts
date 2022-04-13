import { PatientData } from "../../Api/PatientsData";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../ActionCreators/PatientActionCreators";
import { GETTINGPATIENT, GOTPATIENT } from "../Actions/PatientActions";

type PatientsActions =
  | ReturnType<typeof gettingPatientAction>
  | ReturnType<typeof gotPatientAction>;

export interface PatientsState {
  readonly loading: boolean;
  readonly patient?: PatientData | null;
}

const initialPatientsState: PatientsState = {
  loading: false,
  patient: undefined,
};

export const patientsReducer = (
  state = initialPatientsState,
  action: PatientsActions,
) => {
  switch (action.type) {
    case GETTINGPATIENT: {
      return {
        ...state,
        patient: null,
        loading: true,
      };
    }
    case GOTPATIENT: {
      return {
        ...state,
        patient: action.patient,
        loading: false,
      };
    }
  }
  return state;
};
