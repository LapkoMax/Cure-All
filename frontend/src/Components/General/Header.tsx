/** @jsxImportSource @emotion/react */
import { Fragment, useEffect } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import {
  Link,
  useLocation,
  useNavigate,
  useSearchParams,
} from "react-router-dom";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  headerContainer,
  helloUserLabel,
  searchInput,
  signInAnchor,
  titleAnchor,
} from "../../Styles/General/HeaderStyles";
import { UserIcon } from "./Icons";

type FormData = {
  searchTerm: string;
};

export const Header = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const user = useSelector((state: AppState) => state.identity.user);
  const { register, handleSubmit } = useForm<FormData>();
  const [searchParams] = useSearchParams();
  const searchTerm = searchParams.get("fullNameSearchTerm") || "";
  const navigate = useNavigate();

  useEffect(() => {
    if (
      user === null &&
      !window.location.href.toString().includes("signin") &&
      !window.location.href.toString().includes("register")
    ) {
      navigate("signin");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [window.location.href, user]);

  const submitForm = ({ searchTerm }: FormData) => {
    searchTerm = searchTerm.trim();
    if (searchTerm !== "") navigate("search?fullNameSearchTerm=" + searchTerm);
  };

  return (
    <div css={headerContainer} className="d-flex justify-content-around">
      <Link
        to="/"
        css={titleAnchor}
        className={
          (user === null ? "col-11" : "col-3") +
          " row d-flex justify-content-start"
        }
      >
        Cure-All
      </Link>
      {user !== null && (
        <Fragment>
          <form
            className="col-3 row d-flex justify-content-center"
            onSubmit={handleSubmit(submitForm)}
          >
            <input
              {...register("searchTerm")}
              type="text"
              placeholder="Поиск докторов..."
              defaultValue={searchTerm}
              css={searchInput}
            />
          </form>
          <div className="col-3 row d-flex justify-content-end">
            <Link
              to={`profile/${user?.id}`}
              css={helloUserLabel}
              className="col-6 row d-flex justify-content-end"
            >
              <span className="col-2">
                <UserIcon />
              </span>
              {user?.userName}
            </Link>
            <Link
              to=""
              onClick={() => {
                dispatch(signOutUserAction(location.pathname));
              }}
              css={signInAnchor}
              className="col-4 row d-flex justify-content-end"
            >
              <span className="col-8 row">Выход</span>
            </Link>
          </div>
        </Fragment>
      )}
    </div>
  );
};
