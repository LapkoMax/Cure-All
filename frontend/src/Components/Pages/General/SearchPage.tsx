/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useSearchParams } from "react-router-dom";
import { getFastSearchedDoctors } from "../../../Api/DoctorsData";
import {
  gettingDoctorsAction,
  gotDoctorsAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { forSearch } from "../../../Styles/SearchPageStyles";
import { DoctorList } from "../../Doctors/DoctorList";
import { Page } from "../../General/Page";

export const SearchPage = () => {
  const [searchParams] = useSearchParams();
  const searchTerm = searchParams.get("searchTerm") || "";
  const dispatch = useDispatch();
  const location = useLocation();
  const doctors = useSelector((state: AppState) => state.doctors.doctors);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const doctorsLoading = useSelector(
    (state: AppState) => state.doctors.loading,
  );

  useEffect(() => {
    const doSearch = async (fullNameSearchTerm: string) => {
      dispatch(gettingDoctorsAction());
      const result = await getFastSearchedDoctors(searchTerm, userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname + location.search));
      dispatch(gotDoctorsAction(result.data));
    };
    doSearch(searchTerm);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchTerm]);

  return (
    <Page title="Результаты поиска">
      {searchTerm && <p css={forSearch}>для "{searchTerm}"</p>}
      {doctorsLoading ? <div>Загрузка...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
