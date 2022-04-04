/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { getDoctors } from "../Api/DoctorsData";
import { PrimaryButton } from "../Styles/Common/Buttons";
import { DoctorList } from "./Doctors/DoctorList";
import { Page } from "./General/Page";
import { PageTitle } from "./General/PageTitle";
import { titleContainer } from "../Styles/HomePageStyles";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../Store/ActionCreators/DoctorActionCreators";

export const HomePage = () => {
  const dispatch = useDispatch();
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );

  useEffect(() => {
    const doGetDoctors = async () => {
      dispatch(gettingDoctorsAction());
      let results = await getDoctors();
      dispatch(gotDoctorsAction(results));
    };
    doGetDoctors();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page>
      <div css={titleContainer}>
        <PageTitle>Doctors</PageTitle>
        <PrimaryButton>Create new doctor</PrimaryButton>
      </div>
      {doctorsLoading ? <div>Loading...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
