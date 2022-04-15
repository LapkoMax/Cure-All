/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { getDoctors } from "../../../Api/DoctorsData";
import { DoctorList } from "../../Doctors/DoctorList";
import { Page } from "../../General/Page";
import { PageTitle } from "../../General/PageTitle";
import { titleContainer } from "../../../Styles/HomePageStyles";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { useLocation } from "react-router-dom";

export const HomePage = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );

  useEffect(() => {
    const doGetDoctors = async () => {
      dispatch(gettingDoctorsAction());
      let result = await getDoctors(userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorsAction(result.data));
    };
    doGetDoctors();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Page>
      <div css={titleContainer}>
        <PageTitle>Список докторов</PageTitle>
      </div>
      {doctorsLoading ? <div>Загрузка...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
