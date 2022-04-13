import { PatientCardData } from "../../Api/PatientCardsData";
import {
  gettingPatientCardAction,
  gotPatientCardAction,
} from "../ActionCreators/PatientCardActionCreators";
import {
  GETTINGPATIENTCARD,
  GOTPATIENTCARD,
} from "../Actions/PatientCardActions";

type PatientCardsActions =
  | ReturnType<typeof gettingPatientCardAction>
  | ReturnType<typeof gotPatientCardAction>;

export interface PatientCardsState {
  readonly loading: boolean;
  readonly patientCard?: PatientCardData | null;
}

const initialPatientCardsState: PatientCardsState = {
  loading: false,
  patientCard: undefined,
};

export const patientCardsReducer = (
  state = initialPatientCardsState,
  action: PatientCardsActions,
) => {
  switch (action.type) {
    case GETTINGPATIENTCARD: {
      return {
        ...state,
        patientCard: null,
        loading: true,
      };
    }
    case GOTPATIENTCARD: {
      return {
        ...state,
        patientCard: action.patientCard,
        loading: false,
      };
    }
  }
  return state;
};
